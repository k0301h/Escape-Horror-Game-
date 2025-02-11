using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour
{
    #region Cached references
    
    private static readonly int IndexWalk = Animator.StringToHash("IndexWalk");
    
    private static readonly int TWalk = Animator.StringToHash("tWalk");
    private static readonly int WalkSpeed = Animator.StringToHash("WalkSpeed");

    #endregion
    
    [SerializeField] private Camera _viewCamera;
    [SerializeField] private CharacterController _cc;
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private PlayerIKController _IKController;
    [SerializeField] private Animator _animator;
    
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _FurnitureImage;
    [SerializeField] private GameObject _CursorImage;
    [SerializeField] private GameObject _LockImage;
    [SerializeField] private GameObject _ItemImage;
    
    [SerializeField] private FlashLight _flashLight;
    
    #region controll variables
    [SerializeField] private float _moveSpeed = 3f;
    
    [SerializeField] private float sensitivityX = 3f;
    [SerializeField] private float sensitivityY = 2f;
    [SerializeField] private float minimumY = -50.0f;
    [SerializeField] private float maximumY = 50.0f;
    private float _rotationX = 0.0f;
    private float _rotationY = 0.0f;
    
    private bool _isMouseLocked;
    private bool _isRun;
    
    [SerializeField] private float jumpHight = 1.5f;
    private const float Gravity = -9.8f;
    private Vector3 _gravityVelocity;
    #endregion
    
    #region Ray Variables
    private RaycastHit _furnitureHit;
    private RaycastHit _itemHit;
    private RaycastHit _targetHit;

    private readonly float Ray_Dist = 2.0f;
    
    private readonly int Layer_Target = 1 << 10;
    #endregion
    
    void Start()
    {
        // extension class 사용법
        // Object timer = ObjectExtension.FindObjectByID("TimeManager");
        // _timer = timer?.GetComponent<TimeManager>();
        
        _viewCamera = gameObject.GetComponentInChildren<Camera>();
        _cc = gameObject.GetComponent<CharacterController>();
        _canvas = gameObject.GetComponentInChildren<Canvas>();
        
        var image = _canvas.GetComponentsInChildren<RawImage>();

        _FurnitureImage = image[1].transform.parent.gameObject;
        _CursorImage = image[1].gameObject;
        _LockImage = image[2].gameObject;
        _ItemImage = image[4].transform.parent.gameObject;
        
        _FurnitureImage.SetActive(false);
        _CursorImage.SetActive(false);
        _LockImage.SetActive(false);
        _ItemImage.SetActive(false);
        
        _inventory = gameObject.GetComponentInChildren<PlayerInventory>();
        _IKController = gameObject.GetComponent<PlayerIKController>();
        _animator = gameObject.GetComponent<Animator>();

        _isMouseLocked = false;
        _isRun = false;
    }

    void Update()
    {
        #region Move Function
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            WalkingAnimation();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            WalkingAnimation();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            WalkingAnimation();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            WalkingAnimation();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _isRun = true;
            _animator.SetFloat(WalkSpeed, 1.5f);
        }
        
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += new Vector3(Mathf.Sin(transform.localEulerAngles.y * Mathf.Deg2Rad), 0,
                Mathf.Cos(transform.localEulerAngles.y * Mathf.Deg2Rad));
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += new Vector3(Mathf.Sin((transform.localEulerAngles.y + 180.0f) * Mathf.Deg2Rad), 0,
                Mathf.Cos((transform.localEulerAngles.y + 180.0f) * Mathf.Deg2Rad));
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += new Vector3(Mathf.Sin((transform.localEulerAngles.y + 270.0f) * Mathf.Deg2Rad), 0,
                Mathf.Cos((transform.localEulerAngles.y + 270.0f) * Mathf.Deg2Rad));
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += new Vector3(Mathf.Sin((transform.localEulerAngles.y + 90.0f) * Mathf.Deg2Rad), 0,
                Mathf.Cos((transform.localEulerAngles.y + 90.0f) * Mathf.Deg2Rad));
        }

        if (moveDirection != Vector3.zero)
        {
            moveDirection = moveDirection.normalized;
        }
        
        if (_isRun)
        {
            moveDirection *= 1.5f;
        }
        
        #region Gravity

        if (_cc.isGrounded && _gravityVelocity.y < 0)
        {
            _gravityVelocity.y = -2f;
        }

        #region Jump
        
        // TODO : 일단 만들자
        // if (Input.GetKeyDown(KeyCode.Space) && _cc.isGrounded)
        // {
        //     _gravityVelocity.y = Mathf.Sqrt(jumpHight * -2f * Gravity);
        // }
        #endregion
        
        _gravityVelocity.y += Gravity * (TimeManager.Instance == null ? Time.deltaTime : TimeManager.Instance.DeltaTime());
        _cc.Move(_gravityVelocity * (TimeManager.Instance == null ? Time.deltaTime : TimeManager.Instance.DeltaTime()));
        #endregion
        
        _cc.Move(_moveSpeed * (TimeManager.Instance == null ? Time.deltaTime : TimeManager.Instance.DeltaTime()) * moveDirection);
        
        if (Input.GetKeyUp(KeyCode.W))
        {
            _animator.SetInteger(IndexWalk, _animator.GetInteger(IndexWalk) - 1);
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            _animator.SetInteger(IndexWalk, _animator.GetInteger(IndexWalk) - 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            _animator.SetInteger(IndexWalk, _animator.GetInteger(IndexWalk) - 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            _animator.SetInteger(IndexWalk, _animator.GetInteger(IndexWalk) - 1);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isRun = false;
            _animator.SetFloat(WalkSpeed, 1.0f);
        }
        

        #region Demo Moving1

        // if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) ||
        //     Input.GetKeyDown(KeyCode.D))
        // {
        //     WalkingAnimation();
        // }
        //
        // //
        // if (Input.GetKey(KeyCode.A))
        // {
        //     _cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * new Vector3(Mathf.Sin((transform.localEulerAngles.y + 270.0f) * Mathf.Deg2Rad), 0,
        //         Mathf.Cos((transform.localEulerAngles.y + 270.0f) * Mathf.Deg2Rad)));
        // }
        // if (Input.GetKey(KeyCode.D))
        // {
        //     _cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * new Vector3(Mathf.Sin((transform.localEulerAngles.y + 90.0f) * Mathf.Deg2Rad), 0,
        //         Mathf.Cos((transform.localEulerAngles.y + 90.0f) * Mathf.Deg2Rad)));
        // }
        // if (Input.GetKey(KeyCode.W))
        // {
        //     _cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * new Vector3(Mathf.Sin(transform.localEulerAngles.y * Mathf.Deg2Rad), 0,
        //         Mathf.Cos(transform.localEulerAngles.y * Mathf.Deg2Rad)));
        // }
        // if (Input.GetKey(KeyCode.S))
        // {            
        //     _cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * new Vector3(Mathf.Sin((transform.localEulerAngles.y + 180.0f) * Mathf.Deg2Rad), 0,
        //         Mathf.Cos((transform.localEulerAngles.y + 180.0f) * Mathf.Deg2Rad)));
        // }
        // //
        //
        // if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) ||
        //     Input.GetKeyUp(KeyCode.D))
        // {
        //     _animator.SetInteger(IndexWalk, _animator.GetInteger(IndexWalk) - 1);
        // }
        //

        #endregion
        #region Demo Moving2
        
        // if (Input.GetKeyDown(KeyCode.W))
        // {
        //     WalkingAnimation();
        //     isFornt = !isFornt;
        //     DebugManager.Instance.Log($"isFornt : {isFornt}");
        // }
        // if (Input.GetKeyDown(KeyCode.S))
        // {
        //     WalkingAnimation();
        //     isBack = !isBack;
        //     DebugManager.Instance.Log($"isBack : {isBack}");
        // }
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     WalkingAnimation();
        //     isLeft = !isLeft;
        //     DebugManager.Instance.Log($"isLeft : {isLeft}");
        // }
        // if (Input.GetKeyDown(KeyCode.D))
        // {
        //     WalkingAnimation();
        //     isRight = !isRight;
        //     DebugManager.Instance.Log($"isRight : {isRight}");
        // }
        //
        // // 여기서 boolean값 이용해서 구현하려 했으나 더 효율적인 방안 구안
        // if (isFornt)
        // {
        //     _cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * new Vector3(Mathf.Sin(transform.localEulerAngles.y * Mathf.Deg2Rad), 0,
        //         Mathf.Cos(transform.localEulerAngles.y * Mathf.Deg2Rad)));
        // }
        // if (isBack)
        // {            
        //     _cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * new Vector3(Mathf.Sin((transform.localEulerAngles.y + 180.0f) * Mathf.Deg2Rad), 0,
        //         Mathf.Cos((transform.localEulerAngles.y + 180.0f) * Mathf.Deg2Rad)));
        // }
        // if (isLeft)
        // {
        //     _cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * new Vector3(Mathf.Sin((transform.localEulerAngles.y + 270.0f) * Mathf.Deg2Rad), 0,
        //         Mathf.Cos((transform.localEulerAngles.y + 270.0f) * Mathf.Deg2Rad)));
        // }
        // if (isRight)
        // {
        //     _cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * new Vector3(Mathf.Sin((transform.localEulerAngles.y + 90.0f) * Mathf.Deg2Rad), 0,
        //         Mathf.Cos((transform.localEulerAngles.y + 90.0f) * Mathf.Deg2Rad)));
        // }
        // //
        //
        // if (Input.GetKeyUp(KeyCode.W))
        // {
        //     _animator.SetInteger(IndexWalk, _animator.GetInteger(IndexWalk) - 1);
        //     isFornt = !isFornt;
        //     DebugManager.Instance.Log($"isFornt : {isFornt}");
        // }
        // if (Input.GetKeyUp(KeyCode.S))
        // {
        //     _animator.SetInteger(IndexWalk, _animator.GetInteger(IndexWalk) - 1);
        //     isBack = !isBack;
        //     DebugManager.Instance.Log($"isBack : {isBack}");
        // }
        // if (Input.GetKeyUp(KeyCode.A))
        // {
        //     _animator.SetInteger(IndexWalk, _animator.GetInteger(IndexWalk) - 1);
        //     isLeft = !isLeft;
        //     DebugManager.Instance.Log($"isLeft : {isLeft}");
        // }
        // if (Input.GetKeyUp(KeyCode.D))
        // {
        //     _animator.SetInteger(IndexWalk, _animator.GetInteger(IndexWalk) - 1);
        //     isRight = !isRight;
        //     DebugManager.Instance.Log($"isRight : {isRight}");
        // }
        
        #endregion
        #endregion
    }
    
    void FixedUpdate()
    {
        #region Mouse Function

        #region Direction
        
        _rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        
        _rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        _rotationY = Mathf.Clamp(_rotationY, minimumY, maximumY);
        
        transform.localEulerAngles = new Vector3(0.0f, _rotationX, 0.0f);
        _viewCamera.transform.localEulerAngles = new Vector3(-_rotationY, 0.0f, 0.0f);

        #endregion
        
        #region Click Function
        
        bool RayResult = Physics.Raycast(_viewCamera.transform.position, _viewCamera.transform.forward, out _targetHit, Ray_Dist,
            Layer_Target);
        
        // always
        if (RayResult)
        {
            var rayObject = _targetHit.collider.gameObject;

            if (rayObject.TryGetComponent<Item>(out Item item))
            {
                _ItemImage.SetActive(true);
                _FurnitureImage.SetActive(false);
            }
            else if (rayObject.TryGetComponent<Door_Controller>(out Door_Controller doorController))
            {
                _FurnitureImage.SetActive(true);
                _ItemImage.SetActive(false);
                
                if (doorController.IsLock())
                {
                    _LockImage.SetActive(true);
                }
                else
                {
                    _CursorImage.SetActive(true);
                }
            }
            else
            {
                _FurnitureImage.SetActive(true);
                _ItemImage.SetActive(false);
                
                _CursorImage.SetActive(true);
            }
        }
        else
        {
            _FurnitureImage.SetActive(false);
            _CursorImage.SetActive(false);
            _LockImage.SetActive(false);
            _ItemImage.SetActive(false);
        }
        //
        
        if (Input.GetMouseButtonDown(0))
        {
            if (RayResult)
            {
                DebugManager.Instance?.LogAndDrawRay(_targetHit, _viewCamera.transform.position, _viewCamera.transform.forward, Ray_Dist);
                
                var rayObject = _targetHit.collider.gameObject;

                if (rayObject.TryGetComponent<Item>(out Item item))
                {
                    // pass
                }
                else if (rayObject.TryGetComponent<Door_Controller>(out Door_Controller doorController))
                {
                    if(doorController.IsOpen())
                        doorController.CloseDoor();
                    else
                        doorController.OpenDoor();
                }
                else if (rayObject.TryGetComponent<Portal>(out Portal portal))
                {
                    portal.LoadScene("1.InGame");
                }
                else if (rayObject.TryGetComponent<EventScript>(out EventScript eventScript))
                {   
                    eventScript.StartEvent();
                }
            }
            else
            {
                DebugManager.Instance?.DrawRay(_viewCamera.transform.position, _viewCamera.transform.forward, Ray_Dist);
            }
        }
        else if (Input.GetMouseButtonDown(2))
        {
            if (_isMouseLocked)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                _isMouseLocked = false;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                _isMouseLocked = true;
            }
        }
        
        #endregion
        
        #region Item

        if (Input.GetKeyDown(KeyCode.E))
        {            
            if (RayResult)
            {
                DebugManager.Instance?.LogAndDrawRay(_targetHit, _viewCamera.transform.position, _viewCamera.transform.forward, Ray_Dist);
                
                var rayObject = _targetHit.collider.gameObject;
                
                if (rayObject.TryGetComponent<Item>(out Item itemCoponent))
                {
                    if (itemCoponent is FlashLight flashLight)
                    {
                        // TODO : 일반화 필요
                        flashLight.Acquired(_viewCamera.gameObject);
                        // flashLight.SetFlash();
                        // _IKController.changeIK();
                    }
                    else
                    {
                        itemCoponent.Acquired(_inventory.gameObject);
                    }
                }
                
                _inventory.AddItem(rayObject);
            }
            else
            {
                DebugManager.Instance?.DrawRay(_viewCamera.transform.position, _viewCamera.transform.forward, Ray_Dist);
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            // TODO : 일반화 필요
            if (_flashLight == null) _flashLight = _inventory._inventory.Find(x => x.name == "Flashlight").GetComponent<FlashLight>();

            if(_flashLight.IsOn())
                _flashLight.TurnOff();
            else 
                _flashLight.TurnOn();  
        }
        #endregion

        #endregion
    }
    
    void WalkingAnimation()
    {
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") == false)
            _animator.SetTrigger(TWalk);
        _animator.SetInteger(IndexWalk, _animator.GetInteger(IndexWalk) + 1);
    }
}
