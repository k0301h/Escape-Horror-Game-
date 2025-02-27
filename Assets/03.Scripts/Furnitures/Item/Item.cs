using UnityEngine;

public struct ITEM_ID
{
    public static readonly int Flash = 1;
}

public class Item : MonoBehaviour
{
    [SerializeField] protected int id;
    
    public virtual void Acquired(GameObject player)
    {
        transform.parent = player.transform;
    }
}
