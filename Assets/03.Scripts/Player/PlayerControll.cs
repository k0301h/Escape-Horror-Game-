using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerControll : MonoBehaviour
{
    public Camera viewCamera;
    public CharacterController cc;
    public Canvas canvas;
    public RawImage _cursorImage;
    
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
    private RaycastHit _hit;

    private readonly float Ray_Dist = 3.0f;
    private readonly int Layer_Furniture = 1 << 10;
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

        // always
        if (Physics.Raycast(transform.position, viewCamera.transform.forward, out _hit, Ray_Dist, Layer_Furniture))
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
            if (Physics.Raycast(transform.position, viewCamera.transform.forward, out _hit, Ray_Dist, Layer_Furniture))
            {
                DebugManager.Instance.LogAndDrawRay(_hit, transform.position, viewCamera.transform.forward, Ray_Dist);
                
                var rayObject = _hit.collider.gameObject;
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
                DebugManager.Instance.DrawRay(transform.position, viewCamera.transform.forward, Ray_Dist);
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
    }
}
