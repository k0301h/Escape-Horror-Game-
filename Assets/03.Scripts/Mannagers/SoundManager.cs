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
