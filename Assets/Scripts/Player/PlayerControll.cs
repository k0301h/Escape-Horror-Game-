using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public Camera ViewCamera;
    public CharacterController cc;
    
    public float _moveSpeed = 1f;
    
    public float sensitivityX = 15.0f;
    public float sensitivityY = 15.0f;

    public float minimumY = -60.0f;
    public float maximumY = 60.0f;
    
    private float _rotationX = 0.0f;
    private float _rotationY = 0.0f;
    
    void Start()
    {
        // extension class 사용법
        // Object timer = ObjectExtension.FindObjectByID("TimeManager");
        // _timer = timer?.GetComponent<TimeManager>();
        
        ViewCamera = gameObject.GetComponentInChildren<Camera>();
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
            cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * Vector3.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * Vector3.right);
        }
        if (Input.GetKey(KeyCode.W))
        {
            cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            cc.Move(_moveSpeed * TimeManager.Instance.DeltaTime() * Vector3.back);
        }
        #endregion
        
        #region Mouse Function

        _rotationX += Input.GetAxis("Mouse X") * sensitivityX;
        
        _rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        _rotationY = Mathf.Clamp(_rotationY, minimumY, maximumY);
        
        ViewCamera.transform.localEulerAngles = new Vector3(-_rotationY, _rotationX, 0.0f);
        
        #endregion
    }
}
