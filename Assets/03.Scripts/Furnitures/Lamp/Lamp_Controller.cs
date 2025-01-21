using System.Collections;
using UnityEngine;

public class Lamp_Controller : MonoBehaviour
{
    public Light light;
    public Material material;

    public bool ison;
    private float value;
    
    void Start()
    {
        light = gameObject.GetComponentInChildren<Light>();
        material = gameObject.GetComponentInChildren<Renderer>().material;

        ison = true;
        value = 0.0f;
        StartCoroutine(LightCoroutines());
    }

    void Update()
    {
        material.SetFloat("_EmissiveExposureWeight", value);
        light.intensity = (1 - value) * 40000;
    }

    IEnumerator LightCoroutines()
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

            yield return new WaitForSeconds(0.01f);
        }
    }
}
