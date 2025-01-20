using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerControll : MonoBehaviour
{
    public Camera viewCamera;
    public CharacterController cc;
    
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
        
        #endregion
    }
}
