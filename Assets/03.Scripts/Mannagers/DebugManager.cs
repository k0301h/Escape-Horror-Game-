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

    // [System.Diagnostics.Conditional("DEBUG_MODE")] : #define DEBUG_MODE 가 선언이 되어있어야함
    // 모든 프로젝트에 적용하려면 Project Setting -> Player -> Scripting Define Symbol에 추가
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public void Log(object message)
    {
    #if UNITY_EDITOR
        if(isDebug)
            Debug.Log(message);
    #endif
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public void DrawRay(Vector3 position, Vector3 direction, float rayDist)
    {
    #if UNITY_EDITOR
        if (isRaycast)
            Debug.DrawRay(position, (direction) * rayDist, Color.red);
    #endif
    }
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public void LogAndDrawRay(RaycastHit hit, Vector3 position, Vector3 direction, float rayDist)
    {
        if (isRaycast)
        {
        #if UNITY_EDITOR
            Log($"hitpoint : {hit.point}, degree : {direction}, distance : {hit.distance}, name : {hit.collider.name}");
            DrawRay(position, direction, rayDist);
        #endif
        }
    }
}