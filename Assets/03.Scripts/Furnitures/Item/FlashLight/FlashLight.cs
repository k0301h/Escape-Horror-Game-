using UnityEngine;

public class FlashLight : Item
{
    private GameObject _light;
    
    private bool _isOn;
    public bool isBreak;

    void Start()
    {
        id = ITEM_ID.Flash;
        _isOn = true;
        isBreak = false;
        _light = transform.Find("Spotlight").gameObject;
    }
    
    // public void SetFlash()
    // {
    //     transform.localPosition = new Vector3(0.317f, -0.139f, 0.422f);
    //     transform.localRotation = Quaternion.Euler(0f, 90f, 90f);
    //
    //     gameObject.AddComponent<FlashLight>();
    //     
    //     BoxCollider boxCollider = GetComponent<BoxCollider>();
    //     Destroy(boxCollider);
    //     
    //     Item thisCoponent = GetComponent<Item>();
    //     Destroy(thisCoponent);
    // }
    
    public override void Acquired(GameObject player)
    {
        base.Acquired(player);
        
        transform.localPosition = new Vector3(0.157f, -0.1f, 0.23f);
        transform.localRotation = Quaternion.Euler(90f, 3f, 0f);

        gameObject.AddComponent<FlashLight>();
        
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Destroy(boxCollider);
        
        Item thisCoponent = GetComponent<Item>();
        Destroy(thisCoponent);
    }

    public void BreakFlash()
    {
        isBreak = true;
        TurnOff();
    }

    public void FixedFlash()
    {
        isBreak = false;
        TurnOn();
    }
    
    public bool IsOn()
    {
        return _isOn;
    }

    public void TurnOn()
    {
        if (!isBreak)
        {
            _isOn = true;
            _light.SetActive(true);
        }
    }

    public void TurnOff()
    {
        _isOn = false;
        _light.SetActive(false);
    }
}