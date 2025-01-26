using System.Collections;
using UnityEngine;

public class Lamp_Controller : MonoBehaviour
{
    public new Light light;
    public Material material;

    private bool ison;
    private float value;
    
    
    public bool now_start_mode;
    public int twinklingMode;
    public int intensity_Amount = 400000;
    
    private Coroutine _lampCoroutine;
    
    void Start()
    {
        light = gameObject.GetComponentInChildren<Light>();
        material = gameObject.GetComponentInChildren<Renderer>().material;
        
        ison = true;
        value = 0.0f;

        if (now_start_mode)
            StartLampTwinkle();
    }

    void Update()
    {
        material.SetFloat("_EmissiveExposureWeight", value);
        light.intensity = (1 - value) * intensity_Amount;
    }

    public void StartLampTwinkle()
    {
        StartCoroutine(RandomStartCoroutines());
    }
    
    public void EndLampTwinkle(bool isOnState)
    {        
        StopCoroutine(_lampCoroutine);

        if (isOnState)
        {
            value = 0.0f;
        }
        else
        {
            value = 1.0f;
        }
    }

    IEnumerator RandomStartCoroutines()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
        
        if (twinklingMode == 0)
            _lampCoroutine = StartCoroutine(MediumLightCoroutines());
        else
            _lampCoroutine = StartCoroutine(FastLightCoroutines());
        
        
    }

    IEnumerator FastLightCoroutines()
    {
        float time = Random.Range(0.5f, 1.0f);
        float breakTime = Random.Range(0.0f, 0.5f);
        while (true)
        {
            if (ison)
            {
                value += 0.1f;
                // 1.0f으로 수정해야함
                if (value >= time)
                {
                    time = Random.Range(0.5f, 1.0f);
                    ison = false;            
                    yield return new WaitForSeconds(breakTime);
                    breakTime = Random.Range(0.0f, 0.5f);
                }
            }
            else
            {
                value -= 0.1f;
                if (value <= 0.0f)
                {
                    time = Random.Range(0.5f, 1.0f);
                    ison = true;
                    yield return new WaitForSeconds(breakTime);
                    breakTime = Random.Range(0.0f, 0.5f);
                }
            }

            yield return new WaitForSeconds(0.002f);
        }
    }
    
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
