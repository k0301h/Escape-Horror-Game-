using UnityEngine;

public class Door_Controller : MonoBehaviour
{
    private static readonly int TOpen = Animator.StringToHash("tOpen");
    private static readonly int TClose = Animator.StringToHash("tClose");
    
    private Animator _animator;
    private bool _isOpen;
    
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _isOpen = false;
    }

    public bool IsOpen()
    {
        return _isOpen;
    }
    
    public void OpenDoor()
    {
        _isOpen = true;
        _animator.SetTrigger(TOpen);
    }
    
    public void CloseDoor()
    {
        _isOpen = false;
        _animator.SetTrigger(TClose);
    }
}
