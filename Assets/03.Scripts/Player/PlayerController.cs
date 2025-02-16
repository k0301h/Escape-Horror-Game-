using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
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
    
    public PlayerUIController _uiController;
    
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
        _viewCamera = gameObject.GetComponentInChildren<Camera>();
        _cc = gameObject.GetComponent<CharacterController>();
        
        _uiController = gameObject.GetComponentInChildren<PlayerUIController>();
        
        _inventory = gameObject.GetComponentInChildren<PlayerInventory>();
        _IKController = gameObject.GetComponent<PlayerIKController>();
        _animator = gameObject.GetComponent<Animator>();

        _isMouseLocked = false;
        _isRun = false;
        
        this.gameObject.AddPlayer();
    }

    void Update()
    {
        #region Move Function
        if (_isMouseLocked)
        {
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

            _gravityVelocity.y +=
                Gravity * (TimeManager.Instance == null ? Time.deltaTime : TimeManager.Instance.DeltaTime());
            _cc.Move(_gravityVelocity *
                     (TimeManager.Instance == null ? Time.deltaTime : TimeManager.Instance.DeltaTime()));

            #endregion

            _cc.Move(_moveSpeed * (TimeManager.Instance == null ? Time.deltaTime : TimeManager.Instance.DeltaTime()) *
                     moveDirection);

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

            if (_animator.GetInteger(IndexWalk) < 0)
            {
                _animator.SetInteger(IndexWalk, 0);
            }
        }
        #endregion
    }
    
    void FixedUpdate()
    {
        #region Mouse Function

        if (_isMouseLocked)
        {
            #region Direction

            _rotationX += Input.GetAxis("Mouse X") * sensitivityX;

            _rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            _rotationY = Mathf.Clamp(_rotationY, minimumY, maximumY);

            transform.localEulerAngles = new Vector3(0.0f, _rotationX, 0.0f);
            _viewCamera.transform.localEulerAngles = new Vector3(-_rotationY, 0.0f, 0.0f);

            #endregion

            #region Click Function

            bool RayResult = Physics.Raycast(_viewCamera.transform.position, _viewCamera.transform.forward,
                out _targetHit, Ray_Dist,
                Layer_Target);

            // always
            if (RayResult)
            {
                var rayObject = _targetHit.collider.gameObject;

                if (rayObject.TryGetComponent<Item>(out Item item))
                {
                    _uiController.SetUI(UI_Index.ItemID, true);
                    _uiController.SetUI(UI_Index.FurnitureID, false);
                }
                else if (rayObject.TryGetComponent<Door_Controller>(out Door_Controller doorController))
                {
                    _uiController.SetUI(UI_Index.FurnitureID, true);
                    _uiController.SetUI(UI_Index.ItemID, false);

                    if (doorController.IsLock())
                    {
                        _uiController.SetUI(UI_Index.LockID, true);
                    }
                    else
                    {
                        _uiController.SetUI(UI_Index.CursorID, true);
                    }
                }
                else
                {
                    _uiController.SetUI(UI_Index.FurnitureID, true);
                    _uiController.SetUI(UI_Index.ItemID, false);

                    _uiController.SetUI(UI_Index.CursorID, true);
                }
            }
            else
            {
                _uiController.SetUI(UI_Index.FurnitureID, false);
                _uiController.SetUI(UI_Index.CursorID, false);
                _uiController.SetUI(UI_Index.LockID, false);
                _uiController.SetUI(UI_Index.ItemID, false);
            }
            //

            if (Input.GetMouseButtonDown(0))
            {
                if (RayResult)
                {
                    DebugManager.Instance?.LogAndDrawRay(_targetHit, _viewCamera.transform.position,
                        _viewCamera.transform.forward, Ray_Dist);

                    var rayObject = _targetHit.collider.gameObject;

                    if (rayObject.TryGetComponent<Item>(out Item item))
                    {
                        // pass
                    }
                    else if (rayObject.TryGetComponent<Door_Controller>(out Door_Controller doorController))
                    {
                        if (doorController.IsOpen())
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
                    DebugManager.Instance?.DrawRay(_viewCamera.transform.position, _viewCamera.transform.forward,
                        Ray_Dist);
                }
            }

            #endregion

            #region Item

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (RayResult)
                {
                    DebugManager.Instance?.LogAndDrawRay(_targetHit, _viewCamera.transform.position,
                        _viewCamera.transform.forward, Ray_Dist);

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
                    DebugManager.Instance?.DrawRay(_viewCamera.transform.position, _viewCamera.transform.forward,
                        Ray_Dist);
                }
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                // TODO : 일반화 필요
                if (_flashLight == null)
                    _flashLight = _inventory._inventory.Find(x => x.name == "Flashlight").GetComponent<FlashLight>();

                if (_flashLight.IsOn())
                    _flashLight.TurnOff();
                else
                    _flashLight.TurnOn();
            }

            #endregion
        }

        if (Input.GetMouseButtonDown(2))
        {
            SetMouseHide();
        }
        #endregion
    }
    
    void WalkingAnimation()
    {
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") == false)
            _animator.SetTrigger(TWalk);
        _animator.SetInteger(IndexWalk, _animator.GetInteger(IndexWalk) + 1);
    }

    public void SetMouseHide()
    {
        if (_isMouseLocked)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _isMouseLocked = false;
            _animator.SetInteger(IndexWalk, 0);
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            _isMouseLocked = true;
        }
    }
}
