using UnityEngine;
using UnityEngine.Events;

public class TimeSlipEvent : MonoBehaviour
{
    public UnityEvent myEvent = new UnityEvent();
    [SerializeField] private AudioSource _audioSource;
    
    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private void DebugSound(bool isPlay)
    {
#if UNITY_EDITOR
        if (SoundManager.Instance == null)
        {
            if(isPlay)
                _audioSource.Play();
            else
                _audioSource.Stop();
        }
#endif
    }
    
    public void PlaySound()
    {
        DebugSound(true);
        
        SoundManager.Instance?.AudioPlay(_audioSource);
    }

    public void StopSound()
    {
        DebugSound(false);
        
        SoundManager.Instance?.AudioStop(_audioSource);
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        myEvent.Invoke();
        
        GetComponent<Collider>().enabled = false;
    }
}
