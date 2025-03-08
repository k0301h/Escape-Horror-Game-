using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Lamp_Controller : MonoBehaviour
{
    public Light lightComponent;
    public Material material;
    [SerializeField] private AudioSource source;
    
    [SerializeField] private bool isOnState = false;
    [SerializeField] private bool now_start_mode;
    [SerializeField] private int twinklingMode;
    [SerializeField] private int intensity_Amount = 400000;
    [SerializeField] private float Emission_Intensity = 10.0f;
    
    private Color Emission_Color;
    [SerializeField] private float Emission_Color_Value = 5.0f;
    
    private Coroutine _lampCoroutine;
    private float value;
    
    void Start()
    {
        lightComponent = gameObject.GetComponentInChildren<Light>();
        material = gameObject.GetComponentInChildren<Renderer>().material;
        source = gameObject.GetComponent<AudioSource>();

        lightComponent.enabled = true;

        if (isOnState)
        {
            value = 0.0f;
            SoundManager.Instance?.AudioPlay(source);
        }
        else
            value = 1.0f;

        Emission_Color = new Color(1, 1, 1);
        
        if (now_start_mode)
            StartLampTwinkle();
    }

    void Update()
    {
        // 이렇게 하면 Emission Color가 너무 쌔다
        // Color finalEmissionColor = new Color(1, 1, 1);
        // finalEmissionColor *= (1 - value) * 10.0f;
        // material.SetColor("_EmissiveColor", finalEmissionColor);
        
        material.SetColor("_EmissiveColor", Emission_Color * Emission_Color_Value * (1 - value));
        
        material.SetFloat("_EmissiveExposureWeight", value);
        material.SetFloat("_EmissiveIntensity", value * Emission_Intensity); 
        
        lightComponent.intensity = (1 - value) * intensity_Amount;
    }

    public void StartLampTwinkle()
    {
        StartCoroutine(RandomStartCoroutines());
    }
    
    public void EndLampTwinkle(bool state)
    {        
        StopCoroutine(_lampCoroutine);
        lightComponent.color = Color.white;
        Emission_Color = new Color(1, 1, 1);

        if (state)
        {
            value = 0.0f;
            SoundManager.Instance?.AudioPlay(source);
        }
        else
        {
            value = 1.0f;
            SoundManager.Instance?.AudioStop(source);
        }
    }

    IEnumerator RandomStartCoroutines()
    {
        if (twinklingMode == 0)
            _lampCoroutine = StartCoroutine(LightCoroutines());
        else if(twinklingMode == 1)
            _lampCoroutine = StartCoroutine(FastLightCoroutines());
        else if (twinklingMode == 2)
        {
            _lampCoroutine = StartCoroutine(FastLightCoroutines());
            lightComponent.color = Color.red;
            Emission_Color = new Color(1, 0, 0);
        }
        yield return null;
    }

    IEnumerator FastLightCoroutines()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
        
        float time = Random.Range(0.5f, 1.0f);
        float breakTime = Random.Range(0.0f, 0.5f);
        bool isOn = true;
        
        while (true)
        {
            if (isOn)
            {
                value += Random.Range(0.1f, 0.3f);
                // 1.0f으로 수정해야함
                if (value >= time)
                {
                    time = Random.Range(0.5f, 1.0f);
                    value = 1.0f;
                    isOn = false;            
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
                    isOn = true;
                    yield return new WaitForSeconds(breakTime);
                    breakTime = Random.Range(0.0f, 1.0f);
                }
            }

            yield return new WaitForSeconds(0.002f);
        }
    }
    
    IEnumerator LightCoroutines()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, 1.0f));
        
        bool isOn = true;
        
        while (true)
        {
            if (isOn)
            {
                value += 0.1f;
                if (value >= 1.0f) isOn = false;
            }
            else
            {
                value -= 0.1f;
                if (value <= 0.0f) isOn = true;
            }

            yield return new WaitForSeconds(0.03f);
        }
    }
}
