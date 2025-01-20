using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour, ISingleton
{
    private double _time, _prev;
    
    void Start()
    {
        ObjectExtension.Log();
        _time = 0;
    }

    void Update()
    {
        _prev = _time;
        _time += Time.deltaTime;
    }

    double GetDeltaTime()
    {
        return _time - _prev;
    }
    
    double GetTime()
    {
        return _time;
    }
}
