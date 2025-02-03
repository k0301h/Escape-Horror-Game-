using UnityEngine;

public class VolumeController : MonoBehaviour
{
    public void StartVolume()
    {
        BoxCollider boxCollider = this.GetComponent<BoxCollider>();
        boxCollider.enabled = true;
    }
    
    public void EraseVolume()
    {
        gameObject.SetActive(false);
    }
}