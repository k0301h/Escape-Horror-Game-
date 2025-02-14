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
        // PlayerExtension.FindPlayerByID("player");

        GameObject player = PlayerExtension.FindPlayerByID("player1").GameObject();
        var playerContoroller = player.GetComponentInChildren<PlayerController>();
        
        playerContoroller.StoryBackGroundImage.SetActive(true);
        
        
        
    }
}