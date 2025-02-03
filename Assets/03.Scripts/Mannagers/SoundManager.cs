using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    
    [SerializeField] private bool isOn;

    void Awake()
    {
        if(_instance == null)
            _instance = this;
    }

    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
                return null;

            return _instance;
        }
    }

    IEnumerator SoundAutoPlayCoroutine(AudioSource audioSource, float time)
    {
        audioSource.Play();
        yield return new WaitForSeconds(time);
        audioSource.Stop();
    }
    
    public void BGMPlay(AudioSource audioSource)
    {
        if (isOn)
        {
            StartCoroutine(SoundAutoPlayCoroutine(audioSource, 10.0f));
        }
    }

    public void AudioPlay(AudioSource audioSource)
    {
        if(isOn)
            audioSource.Play();
    }

    public void AudioStop(AudioSource audioSource)
    {
        if(isOn)
            audioSource.Stop();
    }
}
