using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private static TimeManager _instance;
    
    private float _time, _prev;
    private bool _isRunning;

    void Awake()
    {
        _isRunning = true;
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
        if (_isRunning)
        {
            _prev = _time;
            _time += Time.deltaTime;
        }
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
        if(_isRunning)
            return _time - _prev;
        
        return 0f;
    }
    
    public float GetTotalTime()
    {
        return _time;
    }

    public bool Running
    {
        get => _isRunning;
        set => _isRunning = value;
    }
}
