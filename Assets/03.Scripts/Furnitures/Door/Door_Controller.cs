using UnityEngine;

public class Door_Controller : MonoBehaviour
{
    private static readonly int TOpen = Animator.StringToHash("tOpen");
    private static readonly int TClose = Animator.StringToHash("tClose");
    
    private Animator _animator;
    private AudioSource _openAudioSource;
    private AudioSource _closeAudioSource;
    private AudioSource _LockAudioSource;
    private AudioSource _LockOffAudioSource;
    private bool _isOpen;

    [SerializeField] private bool isLock = true;
    
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        // _openClip = Resources.Load<AudioClip>("10.Sounds/door/door_open");
        
        var audios = gameObject.GetComponentsInChildren<AudioSource>();

        _openAudioSource = audios[0];
        _closeAudioSource = audios[1];
        _LockAudioSource = audios[2];
        _LockOffAudioSource = audios[3];
        
        // _openAudioSource = gameObject.transform.GetChild(2).GetComponent<AudioSource>();
        // _closeAudioSource = gameObject.transform.GetChild(3).GetComponent<AudioSource>();
        // _LockAudioSource = gameObject.transform.GetChild(4).GetComponent<AudioSource>();
        // _LockOffAudioSource = gameObject.transform.GetChild(5).GetComponent<AudioSource>();
        _isOpen = false;
    }

    public bool IsOpen()
    {
        return _isOpen;
    }

    public bool IsLock()
    {
        return isLock;
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    private void DebugSound(AudioSource audioSource)
    {
        #if UNITY_EDITOR
        if (SoundManager.Instance == null)
        {
            audioSource.Play();
        }
        #endif
    }

    public void LockOffDoor()
    {
        DebugSound(_LockOffAudioSource);

        SoundManager.Instance?.AudioPlay(_LockOffAudioSource);
        isLock = false;
    }
    
    public void OpenDoor()
    {
        if (!isLock)
        {
            DebugSound(_openAudioSource);

            SoundManager.Instance?.AudioPlay(_openAudioSource);
            _isOpen = true;
            _animator.SetTrigger(TOpen);
        }
        else
        {
            DebugSound(_LockAudioSource);

            SoundManager.Instance?.AudioPlay(_LockAudioSource);
        }
    }

    public void CloseDoor()
    {
        DebugSound(_closeAudioSource);
        
        SoundManager.Instance?.AudioPlay(_closeAudioSource);
        _isOpen = false;
        _animator.SetTrigger(TClose);
    }
}
