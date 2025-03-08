using UnityEngine;

public class LightController : MonoBehaviour
{
    private Light _light;
    
    void Start()
    {
        _light = GetComponent<Light>();
    }

    public void SetColor(string color)
    {
        if(color == "Red")
            _light.color = Color.red;
        else if (color == "White")
            _light.color = Color.white;
    }
}
