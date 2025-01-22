using System.Linq;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int id;

    public bool IsFlash()
    {
        if(id == 1)
            return true;
        
        return false;
    }

    public void SetFlash()
    {
        transform.localPosition = new Vector3(0.317f, -0.139f, 0.422f);
        transform.localRotation = Quaternion.Euler(0f, 90f, 90f);
        
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Destroy(boxCollider);
        
        Item thisCoponent = GetComponent<Item>();
        Destroy(thisCoponent);
    }
    
    public void Acquired(GameObject player)
    {
        transform.parent = player.transform;
    }
}
