using System;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    private static DebugManager _instance;

    public bool isDebug = true;
    public bool isRaycast = true;
    

    private void Awake()
    {
        if(_instance == null)
            _instance = this;
        else
            Destroy(this.gameObject);
    }

    private void OnValidate()
    {
        if (!isDebug)
        {
            isRaycast = false;
        }
    }

    public static DebugManager Instance
    {
        get
        {
            if (_instance == null)
                return null;
            
            return _instance;
        }
    }

    public void Log(object message)
    {
        Debug.Log(message);
    }
    
    public void DrawRay(Vector3 position, Vector3 direction, float rayDist)
    {
        if (isRaycast)
            Debug.DrawRay(position, (direction) * rayDist, Color.red);
    }
    
    public void LogAndDrawRay(RaycastHit hit, Vector3 position, Vector3 direction, float rayDist)
    {
        if (isRaycast)
        {
            Debug.Log($"hitpoint : {hit.point}, degree : {direction}, distance : {hit.distance}, name : {hit.collider.name}");
            DrawRay(position, direction, rayDist);
        }
    }
}