using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public CharacterController cc;
    private float _moveSpeed = 1f;
    
    private TimeManager _timer;
    
    void Start()
    {
        Object timer = ObjectExtension.FindObjectByID("TimeManager");
        _timer = timer?.GetComponent<TimeManager>();
        
        cc = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            cc.Move(_moveSpeed * Time.deltaTime * Vector3.left);
        }
    }
}
