using UnityEngine;

public class FlashLight : Item
{
    private GameObject _light;
    private bool ison;

    void Start()
    {
        id = 1;
        ison = true;
        _light = transform.Find("Spotlight").gameObject;
    }
    
    public void SetFlash()
    {
        transform.localPosition = new Vector3(0.317f, -0.139f, 0.422f);
        transform.localRotation = Quaternion.Euler(0f, 90f, 90f);

        gameObject.AddComponent<FlashLight>();
        
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Destroy(boxCollider);
        
        Item thisCoponent = GetComponent<Item>();
        Destroy(thisCoponent);
    }
    
    public override void Acquired(GameObject player)
    {
        base.Acquired(player);
        
        transform.localPosition = new Vector3(0.317f, -0.139f, 0.422f);
        transform.localRotation = Quaternion.Euler(0f, 90f, 90f);

        gameObject.AddComponent<FlashLight>();
        
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Destroy(boxCollider);
        
        Item thisCoponent = GetComponent<Item>();
        Destroy(thisCoponent);
    }

    public bool IsOn()
    {
        return ison;
    }
    
    public void TurnOn()
    {
        ison = true;
        _light.SetActive(true);
    }

    public void TurnOff()
    {
        ison = false;
        _light.SetActive(false);
    }
}