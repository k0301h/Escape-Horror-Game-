using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerControll : MonoBehaviour
{
    public Camera viewCamera;
    public CharacterController cc;
    private RaycastHit _hit;
    
    public float _moveSpeed = 1f;
    
    public float sensitivityX = 15.0f;
    public float sensitivityY = 15.0f;

    public float minimumY = -50.0f;
    public float maximumY = 50.0f;
    
    private float _rotationX = 0.0f;
    private float _rotationY = 0.0f;
    
    void Start()
    {
        // extension class 사용법
        // Object timer = ObjectExtension.FindObjectByID("TimeManager");
        // _timer = timer?.GetComponent<TimeManager>();
        
        viewCamera = gameObject.GetComponentInChildren<Camera>();
        cc = gameObject.GetComponent<CharacterController>();
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

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, viewCamera.transform.forward, out _hit, 5.0f))
            {
                Debug.Log($"hitpoint : {_hit.point}, degree : {viewCamera.transform.forward}, distance : {_hit.distance}, name : {_hit.collider.name}");
                Debug.DrawRay(transform.position, (viewCamera.transform.forward) * 5.0f, Color.red);
                
                var rayObject = _hit.collider.gameObject;
                Door_Controller doorController;

                if (rayObject.TryGetComponent<Door_Controller>(out doorController))
                {
                    if(doorController.IsOpen())
                        doorController.OpenDoor();
                    else
                        doorController.CloseDoor();
                }
            }
            else
            {
                Debug.DrawRay(transform.position, (viewCamera.transform.forward) * 5.0f, Color.red);
            }
        }

        #endregion
    }
}
