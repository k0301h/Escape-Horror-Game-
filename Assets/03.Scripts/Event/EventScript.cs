using Unity.VisualScripting;
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
    
    public void SetFlashLightColor(int colorType)
    {
        PlayerExtension.FindPlayerByID("Player")?.GameObject().GetComponentInChildren<PlayerController>()?.FlashColor(colorType);
    }

    public void PlayMosaic(string colorName)
    {
        var playerUIController = PlayerExtension.FindPlayerByID("Player")?.GameObject().GetComponentInChildren<PlayerUIController>();
        
        playerUIController?.MosaicPlayer(true);
        
        if(colorName == "Red")
            playerUIController?.MosaicColorSet(new Color(1, 0, 0));
        else if(colorName == "Green")
            playerUIController?.MosaicColorSet(new Color(0, 1, 0));
        else if(colorName == "Blue")
            playerUIController?.MosaicColorSet(new Color(0, 0, 1));
    }
    
    public void EndMosaic()
    {
        var playerUIController = PlayerExtension.FindPlayerByID("Player")?.GameObject().GetComponentInChildren<PlayerUIController>();
        
        playerUIController?.MosaicPlayer(false);
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
