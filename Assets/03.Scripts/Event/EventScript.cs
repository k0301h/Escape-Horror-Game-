using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EventScript : MonoBehaviour
{
    // 10초 뒤에 문이 열리도록 다시 구현하자
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

    public void PlayBreath()
    {
        PlayerExtension.FindPlayerByID("Player")?.GameObject().GetComponentInChildren<PlayerSoundPlayer>()?.PlaySound("Breath");
    }
    
    public void StopBreath()
    {
        PlayerExtension.FindPlayerByID("Player")?.GameObject().GetComponentInChildren<PlayerSoundPlayer>()?.StopSound("Breath");
    }
    
    public void StartPaperEvent()
    {
        myEvent.Invoke();
        gameObject.SetActive(false);
    }

    public void FlashLightBreak()
    {
        PlayerExtension.FindPlayerByID("Player")?.GameObject().GetComponentInChildren<PlayerController>()?.FlashBreak();
    }
    
    public void FlashLightFixed()
    {
        PlayerExtension.FindPlayerByID("Player")?.GameObject().GetComponentInChildren<PlayerController>()?.FlashFixed();
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
