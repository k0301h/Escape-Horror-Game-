using Unity.VisualScripting;
using UnityEngine;

public class Paper : Item
{
    [SerializeField] private EventScript eventScript;
    [SerializeField] private string StoryLine;
    
    public override void Acquired(GameObject player)
    {
        base.Acquired(player);

        ShowStory();
    }
    
    private void ShowStory()
    {
        GameObject player = PlayerExtension.FindPlayerByID("Old_Player").GameObject();
        var playerContoroller = player.GetComponentInChildren<PlayerController>();
        var playerUIController = player.GetComponentInChildren<PlayerUIController>();
        
        playerContoroller.SetMouseHide();
        
        playerUIController.SetUI(UI_Index.StoryBackID, true);
        playerUIController.SetUI(UI_Index.StoryLineID, true);
        playerUIController.SetUI(UI_Index.StoryExitButtonID, true);
        
        playerUIController.SetStoryLine(StoryLine);
    }

    public void StartEvent()
    {
        eventScript = gameObject.GetComponent<EventScript>();
        
        eventScript.StartEvent();
        gameObject.SetActive(false);
    }
}