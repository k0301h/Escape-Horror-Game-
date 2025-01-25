using System.Collections;
using UnityEngine;

public class Lamp_Controller : MonoBehaviour
{
    public new Light light;
    public Material material;

    public bool ison;
    private float value;
    public int twinklingMode;
    
    void Start()
    {
        light = gameObject.GetComponentInChildren<Light>();
        material = gameObject.GetComponentInChildren<Renderer>().material;
        
        ison = true;
        value = 0.0f;
        if (twinklingMode == 0)
            StartCoroutine(MediumLightCoroutines());
        else
            StartCoroutine(FastLightCoroutines());
    }

    void Update()
    {
        material.SetFloat("_EmissiveExposureWeight", value);
        light.intensity = (1 - value) * 40000;
    }

    IEnumerator FastLightCoroutines()
    {
        while (true)
        {
            if (ison)
            {
                value += 0.5f;
                if (value >= 1.0f)
                {
                    ison = false;            
                    yield return new WaitForSeconds(0.5f);
                }
            }
            else
            {
                value -= 0.5f;
                if (value <= 0.0f)
                {
                    ison = true;
                    yield return new WaitForSeconds(0.5f);
                }
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
    
    // 따따따따딱닥
    IEnumerator MediumLightCoroutines()
    {
        while (true)
        {
            if (ison)
            {
                value += 0.1f;
                if (value >= 1.0f) ison = false;
            }
            else
            {
                value -= 0.1f;
                if (value <= 0.0f) ison = true;
            }

            yield return new WaitForSeconds(0.03f);
        }
    }
}
