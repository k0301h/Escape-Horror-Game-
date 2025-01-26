using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour
{
    #region Cached references
    
    // private static readonly int IsFront = Animator.StringToHash("isFront");
    // private static readonly int IsRight = Animator.StringToHash("isRight");
    // private static readonly int IsLeft = Animator.StringToHash("isLeft");
    // private static readonly int IsBack = Animator.StringToHash("isBack");
    private static readonly int IndexWalk = Animator.StringToHash("IndexWalk");
    
    private static readonly int TWalk = Animator.StringToHash("tWalk");
    
    #endregion
    
    public Camera _viewCamera;
    public CharacterController _cc;
    public PlayerInventory _inventory;
    public PlayerIKController _IKController;
    public Animator _animator;
    
    public Canvas _canvas;
    public RawImage _cursorImage;
    
    public FlashLight _flashLight;
    
    #region controll variables
    public float _moveSpeed = 1f;
    
    public float sensitivityX = 15.0f;
    public float sensitivityY = 15.0f;
    public float minimumY = -50.0f;
    public float maximumY = 50.0f;
    private float _rotationX = 0.0f;
    private float _rotationY = 0.0f;
    
    private bool _isMouseLocked;
    private bool _isRun;
    #endregion
    
    #region Ray Variables
    private RaycastHit _furnitureHit;
    private RaycastHit _itemHit;

    private readonly float Ray_Dist = 3.0f;
    private readonly int Layer_Furniture = 1 << 10;
    private readonly int Layer_Item = 1 << 11;
    #endregion
    
    void Start()
    {
        // extension class 사용법
        // Object timer = ObjectExtension.FindObjectByID("TimeManager");
        // _timer = timer?.GetComponent<TimeManager>();
        
        _viewCamera = gameObject.GetComponentInChildren<Camera>();
        _cc = gameObject.GetComponent<CharacterController>();
        _canvas = gameObject.GetComponentInChildren<Canvas>();
        _cursorImage = _canvas.gameObject.GetComponentInChildren<RawImage>();
        _inventory = gameObject.GetComponent<PlayerInventory>();
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

        _cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * moveDirection);
        
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
        
        bool furnitureRayResult = Physics.Raycast(_viewCamera.transform.position, _viewCamera.transform.forward, out _furnitureHit, Ray_Dist,
            Layer_Furniture);
        bool ItemRayResult = Physics.Raycast(_viewCamera.transform.position, _viewCamera.transform.forward, out _itemHit, Ray_Dist,
            Layer_Item);
        
        // always
        if (furnitureRayResult || ItemRayResult)
        {
            _cursorImage.enabled = true;
        }
        else
        {
            _cursorImage.enabled = false;
        }
        //
        
        if (Input.GetMouseButtonDown(0))
        {
            if (furnitureRayResult)
            {
                DebugManager.Instance.LogAndDrawRay(_furnitureHit, _viewCamera.transform.position, _viewCamera.transform.forward, Ray_Dist);
                
                var rayObject = _furnitureHit.collider.gameObject;
                Door_Controller doorController;

                if (rayObject.TryGetComponent<Door_Controller>(out doorController))
                {
                    if(doorController.IsOpen())
                        doorController.CloseDoor();
                    else
                        doorController.OpenDoor();
                }
            }
            else
            {
                DebugManager.Instance.DrawRay(_viewCamera.transform.position, _viewCamera.transform.forward, Ray_Dist);
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
            if (ItemRayResult)
            {
                DebugManager.Instance.LogAndDrawRay(_itemHit, _viewCamera.transform.position, _viewCamera.transform.forward, Ray_Dist);
                
                var rayObject = _itemHit.collider.gameObject;
                
                if (rayObject.TryGetComponent<Item>(out Item itemCoponent))
                {
                    if (itemCoponent is FlashLight flashLight)
                    {
                        flashLight.Acquired(_viewCamera.gameObject);
                        flashLight.SetFlash();
                        _IKController.changeIK();
                    }
                }
                
                _inventory.AddItem(rayObject);
            }
            else
            {
                DebugManager.Instance.DrawRay(_viewCamera.transform.position, _viewCamera.transform.forward, Ray_Dist);
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
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
        DebugManager.Instance.Log($"Walk Index : {_animator.GetInteger(IndexWalk)}");
    }
}
