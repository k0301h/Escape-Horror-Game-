using System.Collections;
using UnityEngine;

public class Door_Controller : MonoBehaviour
{
    private static readonly int Open = Animator.StringToHash("tOpen");
    private static readonly int Close = Animator.StringToHash("tClose");
    
    private Animator _animator;
    private AudioSource _openAudioSource;
    private AudioSource _closeAudioSource;
    private AudioSource _lockAudioSource;
    private AudioSource _lockOffAudioSource;
    private AudioSource _lockOnAudioSource;
    private bool _isOpen;

    [SerializeField] private bool isLock = true;
    
    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        
        var audios = gameObject.GetComponentsInChildren<AudioSource>();

        _openAudioSource = audios[0];
        _closeAudioSource = audios[1];
        _lockAudioSource = audios[2];
        _lockOffAudioSource = audios[3];
        _lockOnAudioSource = audios[4];
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
        DebugSound(_lockOffAudioSource);

        SoundManager.Instance?.AudioPlay(_lockOffAudioSource);
        isLock = false;
    }
    
    public void LockOnDoor()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            CloseDoor();
        }
        
        isLock = true;

        StartCoroutine(CloseAndLockDoorCoroutine());
    }

    IEnumerator CloseAndLockDoorCoroutine()
    {
        yield return new WaitForSeconds(0.8f);
        
        DebugSound(_lockOnAudioSource);
        SoundManager.Instance?.AudioPlay(_lockOnAudioSource);
    }

    public void OpenDoor()
    {
        if (!isLock)
        {
            DebugSound(_openAudioSource);

            SoundManager.Instance?.AudioPlay(_openAudioSource);
            _isOpen = true;
            _animator.SetTrigger(Open);
        }
        else
        {
            DebugSound(_lockAudioSource);

            SoundManager.Instance?.AudioPlay(_lockAudioSource);
        }
    }

    public void CloseDoor()
    {
        DebugSound(_closeAudioSource);
        
        SoundManager.Instance?.AudioPlay(_closeAudioSource);
        _isOpen = false;
        _animator.SetTrigger(Close);
    }

    IEnumerator AutoLockOffDoorCoroutine(float time)
    {
        LockOnDoor();
        
        yield return new WaitForSeconds(0.8f);
        
        DebugSound(_lockOnAudioSource);
        SoundManager.Instance?.AudioPlay(_lockOnAudioSource);
        yield return new WaitForSeconds(time);
        LockOffDoor();
    }
    
    public void AutoLockOffDoor(float time)
    {
        StartCoroutine(AutoLockOffDoorCoroutine(time));
    }
    
    IEnumerator AutoOpenDoorCoroutine(float time)
    {
        LockOffDoor();
        yield return new WaitForSeconds(time);
        OpenDoor();
    }
    
    public void AutoOpenDoor(float time)
    {
        StartCoroutine(AutoOpenDoorCoroutine(time));
    }
}
