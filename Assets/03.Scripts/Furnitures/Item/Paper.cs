using Unity.VisualScripting;
using UnityEngine;

public class Paper : Item
{
    private MeshRenderer meshRenderer;
    
    [SerializeField] private EventScript eventScript;
    [SerializeField] private string StoryLine;
    [SerializeField] private AudioSource paperSound;
    
    public override void Acquired(GameObject player)
    {
        base.Acquired(player);

        GetComponent<MeshRenderer>().enabled = false;
        
        SoundManager.Instance.AudioPlay(paperSound);
        ShowStory();
    }
    
    private void ShowStory()
    {
        GameObject player = PlayerExtension.FindPlayerByID("Player").GameObject();
        var playerContoroller = player.GetComponentInChildren<PlayerController>();
        var playerUIController = player.GetComponentInChildren<PlayerUIController>();
        
        playerContoroller.SetMouseHide();
        playerContoroller.IsStoryMode = true;
        
        playerUIController.SetUI(UI_Index.StoryBackID, true);
        playerUIController.SetUI(UI_Index.StoryLineID, true);
        playerUIController.SetUI(UI_Index.StoryExitButtonID, true);
        
        playerUIController.SetStoryLine(StoryLine);
    }
}