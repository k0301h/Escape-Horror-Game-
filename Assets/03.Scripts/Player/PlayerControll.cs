using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour
{
    public Camera viewCamera;
    public CharacterController cc;
    
    public Canvas canvas;
    public RawImage _cursorImage;
    
    public PlayerInventory inventory;
    
    #region controll variables
    public float _moveSpeed = 1f;
    
    public float sensitivityX = 15.0f;
    public float sensitivityY = 15.0f;
    public float minimumY = -50.0f;
    public float maximumY = 50.0f;
    private float _rotationX = 0.0f;
    private float _rotationY = 0.0f;
    
    private bool _isMouseLocked;
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
        
        viewCamera = gameObject.GetComponentInChildren<Camera>();
        cc = gameObject.GetComponent<CharacterController>();
        canvas = gameObject.GetComponentInChildren<Canvas>();
        _cursorImage = canvas.gameObject.GetComponentInChildren<RawImage>();
        inventory = gameObject.GetComponent<PlayerInventory>();

        _isMouseLocked = false;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        #region Move Function
        
        if (Input.GetKey(KeyCode.A))
        {
            cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * new Vector3(Mathf.Sin((transform.localEulerAngles.y + 270.0f) * Mathf.Deg2Rad), 0,
                Mathf.Cos((transform.localEulerAngles.y + 270.0f) * Mathf.Deg2Rad)));
        }
        if (Input.GetKey(KeyCode.D))
        {
            cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * new Vector3(Mathf.Sin((transform.localEulerAngles.y + 90.0f) * Mathf.Deg2Rad), 0,
                Mathf.Cos((transform.localEulerAngles.y + 90.0f) * Mathf.Deg2Rad)));
        }
        if (Input.GetKey(KeyCode.W))
        {
            cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * new Vector3(Mathf.Sin(transform.localEulerAngles.y * Mathf.Deg2Rad), 0,
                Mathf.Cos(transform.localEulerAngles.y * Mathf.Deg2Rad)));
        }
        if (Input.GetKey(KeyCode.S))
        {            
            cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * new Vector3(Mathf.Sin((transform.localEulerAngles.y + 180.0f) * Mathf.Deg2Rad), 0,
                Mathf.Cos((transform.localEulerAngles.y + 180.0f) * Mathf.Deg2Rad)));
        }
        
        #endregion
        
        #region Mouse Function

        _rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        
        _rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        _rotationY = Mathf.Clamp(_rotationY, minimumY, maximumY);
        
        transform.localEulerAngles = new Vector3(0.0f, _rotationX, 0.0f);
        viewCamera.transform.localEulerAngles = new Vector3(-_rotationY, 0.0f, 0.0f);

        bool furnitureRayResult = Physics.Raycast(viewCamera.transform.position, viewCamera.transform.forward, out _furnitureHit, Ray_Dist,
            Layer_Furniture);
        bool ItemRayResult = Physics.Raycast(viewCamera.transform.position, viewCamera.transform.forward, out _itemHit, Ray_Dist,
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
                DebugManager.Instance.LogAndDrawRay(_furnitureHit, viewCamera.transform.position, viewCamera.transform.forward, Ray_Dist);
                
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
                DebugManager.Instance.DrawRay(viewCamera.transform.position, viewCamera.transform.forward, Ray_Dist);
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
        
        #region Item

        if (Input.GetKey(KeyCode.E))
        {            
            if (ItemRayResult)
            {
                DebugManager.Instance.LogAndDrawRay(_itemHit, viewCamera.transform.position, viewCamera.transform.forward, Ray_Dist);
                
                var rayObject = _itemHit.collider.gameObject;
                Item itemCoponent;
                
                inventory.GetItem(rayObject);
                if (rayObject.TryGetComponent<Item>(out itemCoponent))
                {
                    if (itemCoponent.IsFlash())
                    {
                        itemCoponent.Acquired(viewCamera.gameObject);
                        itemCoponent.SetFlash();
                    }
                }
            }
            else
            {
                DebugManager.Instance.DrawRay(viewCamera.transform.position, viewCamera.transform.forward, Ray_Dist);
            }
        }

        #endregion

        #endregion
    }
}
