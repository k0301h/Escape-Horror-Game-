using Unity.VisualScripting;
using UnityEngine;

public class Paper : Item
{
    [SerializeField] private EventScript eventScript;
    
    public override void Acquired(GameObject player)
    {
        base.Acquired(player);

        eventScript = gameObject.GetComponent<EventScript>();
        
        eventScript.StartEvent();
        gameObject.SetActive(false);

        ShowStory();
    }
    
    private void ShowStory()
    {
        GameObject player = PlayerExtension.FindPlayerByID("Old_Player").GameObject();
        var playerContoroller = player.GetComponentInChildren<PlayerController>();
        var playerUIController = player.GetComponentInChildren<PlayerUIController>();
        
        playerUIController.SetUI(UI_Index.StoryBackID, true);
        
        
        
    }
}