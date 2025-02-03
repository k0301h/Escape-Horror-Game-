using UnityEngine;
using UnityEngine.Events;

public class LampTwinkleEvent : MonoBehaviour
{
    public UnityEvent myEvent;
    [SerializeField] private AudioSource _audioSource;
 
    private void Awake() {
        if (myEvent == null)
            myEvent = new UnityEvent();
    }

    private void OnTriggerEnter(Collider other)
    {
        myEvent.Invoke();
        
        GetComponent<Collider>().enabled = false;
    }
    
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

    public void ShowLine(string line)
    {
        LineManager.Instance?.ShowLine(line);
    }
}
