using UnityEngine;

public class Zombie : Creature
{
    private static readonly int SeatIdle = Animator.StringToHash("tSeatIdle");
    private static readonly int SeatClapping = Animator.StringToHash("tSeatClapping");

    public void Play_SeatIdle()
    {
        animator.SetTrigger(SeatIdle);
    }
    
    public void Play_SeatClapping()
    {
        animator.SetTrigger(SeatClapping);
    }
}
