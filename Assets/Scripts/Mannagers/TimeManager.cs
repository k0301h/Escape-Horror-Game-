using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance;
    
    private float _time, _prev;

    void Awake()
    {
        if(_instance == null)
            _instance = this;
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    void Start()
    {
        _time = 0;
    }

    void Update()
    {
        _prev = _time;
        _time += Time.deltaTime;
    }
    
    public static TimeManager Instance
    {
        get 
        {
            if (_instance == null)
                return null;

            return _instance;
        }
    }

    public float DeltaTime()
    {
        return _time - _prev;
    }
    
    public float GetTime()
    {
        return _time;
    }
}
