using UnityEngine;
using UnityEngine.Events;

public class EventScript : MonoBehaviour
{
    [SerializeField] private UnityEvent myEvent;
    
    void Awake() {
        if (myEvent == null)
            myEvent = new UnityEvent();
    }

    public void ShowLine(string line)
    {
        LineManager.Instance?.ShowLine(line);
    }

    public void ShowMap(string mapName)
    {
        LineManager.Instance?.ShowMapName(mapName);
    }

    public void AudioPlay(AudioSource audioSource)
    {
        SoundManager.Instance?.AudioPlay(audioSource);
    }
    
    public void AudioStop(AudioSource audioSource)
    {
        SoundManager.Instance?.AudioStop(audioSource);
    }
    
    public void BGMPlay(AudioSource audioSource)
    {
        SoundManager.Instance?.BGMPlay(audioSource);
    }
    
    public void StartPaperEvent()
    {
        myEvent.Invoke();
        gameObject.SetActive(false);
    }
    
    public void StartEvent()
    {
        myEvent.Invoke();
        
        // gameObject.SetActive(false);
        var col = GetComponent<Collider>();
        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        myEvent.Invoke();
        
        // gameObject.SetActive(false);
        var cols = GetComponents<Collider>();
        foreach (var col in cols)
        {
            col.enabled = false;
        }
    }
}
