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
    
    // TODO : 추후에 Inventory Array에 못 찾는 경우가 발생할 수 있으니 재확인 필요
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
        
        gameObject.SetActive(false);
    }
}
