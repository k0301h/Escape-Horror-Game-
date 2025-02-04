using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Lamp_Controller : MonoBehaviour
{
    public new Light light;
    public Material material;

    [SerializeField] private bool isOnState = false;
    private float value;
    
    public bool now_start_mode;
    public int twinklingMode;
    public int intensity_Amount = 400000;
    
    private Coroutine _lampCoroutine;
    
    void Start()
    {
        light = gameObject.GetComponentInChildren<Light>();
        material = gameObject.GetComponentInChildren<Renderer>().material;

        light.enabled = true;

        if (isOnState)
            value = 0.0f;
        else
            value = 1.0f;

        if (now_start_mode)
            StartLampTwinkle();
    }

    void Update()
    {
        material.SetFloat("_EmissiveExposureWeight", value);
        
        Color finalEmissionColor = new Color(1, 1, 1) * (1 - value) * 10.0f;
        material.SetColor("_EmissiveColor", finalEmissionColor);
        material.SetFloat("_EmissiveIntensity", value * 10.0f); 
        
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
            _lampCoroutine = StartCoroutine(LightCoroutines());
        else
            _lampCoroutine = StartCoroutine(FastLightCoroutines());
        
        
    }

    IEnumerator FastLightCoroutines()
    {
        float time = Random.Range(0.5f, 1.0f);
        float breakTime = Random.Range(0.0f, 0.5f);
        while (true)
        {
            if (isOnState)
            {
                value += Random.Range(0.1f, 0.3f);
                // 1.0f으로 수정해야함
                if (value >= time)
                {
                    time = Random.Range(0.5f, 1.0f);
                    value = 1.0f;
                    isOnState = false;            
                    yield return new WaitForSeconds(breakTime);
                    breakTime = Random.Range(0.0f, 1.0f);
                }
            }
            else
            {
                value -= Random.Range(0.1f, 0.3f);
                if (value <= 0.0f)
                {
                    time = Random.Range(0.5f, 1.0f);
                    value = 0.0f;
                    isOnState = true;
                    yield return new WaitForSeconds(breakTime);
                    breakTime = Random.Range(0.0f, 1.0f);
                }
            }

            yield return new WaitForSeconds(0.002f);
        }
    }
    
    IEnumerator LightCoroutines()
    {
        while (true)
        {
            if (isOnState)
            {
                value += 0.1f;
                if (value >= 1.0f) isOnState = false;
            }
            else
            {
                value -= 0.1f;
                if (value <= 0.0f) isOnState = true;
            }

            yield return new WaitForSeconds(0.03f);
        }
    }
}
