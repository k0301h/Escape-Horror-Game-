using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected int id;

    private static readonly int ID_FLASH = 1;
    
    public virtual void Acquired(GameObject player)
    {
        transform.parent = player.transform;
    }
}
