using System.Collections;
using UnityEngine;

public class Zombie : Creature
{
    [SerializeField] private GameObject player;
    [SerializeField] private CharacterController cc;
    [SerializeField] private float moveSpeed = 3f;
    
    [SerializeField] private AudioSource rightFootSound;
    [SerializeField] private AudioSource leftFootSound;
    [SerializeField] private AudioSource roarSound;
    
    private bool _isRunning;
    
    private static readonly int SeatIdle = Animator.StringToHash("tSeatIdle");
    private static readonly int SeatClapping = Animator.StringToHash("tSeatClapping");
    private static readonly int Run = Animator.StringToHash("tRun");

    #region Find Destination System

    void Start()
    {
        _isRunning = false;
    }

    void FixedUpdate()
    {
        if (_isRunning)
        {
            transform.LookAt(player.transform);

            var direction = (player.transform.position - transform.position).normalized;
            cc.Move(moveSpeed * TimeManager.Instance.DeltaTime() * direction);
        }
    }

    public void StartChase()
    {       
        _isRunning = true;
        SoundManager.Instance.AudioPlay(roarSound);
    }

    #endregion
    
    #region Animation

    public void Play_SeatIdle()
    {
        animator.SetTrigger(SeatIdle);
    }
    
    public void Play_SeatClapping()
    {
        animator.SetTrigger(SeatClapping);
    }

    public void Play_Run()
    {
        animator.SetTrigger(Run);
    }

    public void PlaySound(string soundName)
    {
        if (soundName == "rightFoot")
        {
            SoundManager.Instance?.AudioPlay(rightFootSound);
        }
        else if (soundName == "leftFoot")
        {
            SoundManager.Instance?.AudioPlay(leftFootSound);
        }
    }
    
    #endregion

}
