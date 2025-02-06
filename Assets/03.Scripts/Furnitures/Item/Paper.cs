using UnityEngine;

public class Paper : Item
{
    [SerializeField] private EventScript eventScript;
    
    public override void Acquired(GameObject player)
    {
        base.Acquired(player);

        eventScript = gameObject.GetComponent<EventScript>();
        
        eventScript.StartEvent();
    }
}