using UnityEngine;

public class Door_Controller : MonoBehaviour
{
    private static readonly int TOpen = Animator.StringToHash("tOpen");
    private static readonly int TClose = Animator.StringToHash("tClose");
    
    private Animator _animator;
    // private AudioClip _openClip;
    private AudioSource _closeAudioSource;
    private AudioSource _openAudioSource;
    private bool _isOpen;
    
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        // _openClip = Resources.Load<AudioClip>("10.Sounds/door/door_open");
        _openAudioSource = gameObject.transform.GetChild(2).GetComponent<AudioSource>();
        _closeAudioSource = gameObject.transform.GetChild(3).GetComponent<AudioSource>();
        _isOpen = false;
        
    }

    public bool IsOpen()
    {
        return _isOpen;
    }
    
    public void OpenDoor()
    {
        _openAudioSource.Play();
        _isOpen = true;
        _animator.SetTrigger(TOpen);
    }
    
    public void CloseDoor()
    {
        _closeAudioSource.Play();
        _isOpen = false;
        _animator.SetTrigger(TClose);
    }
}
