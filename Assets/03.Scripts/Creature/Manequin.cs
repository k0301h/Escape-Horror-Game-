using UnityEngine;

public class Manequin : Creature
{
    private static readonly int TRotation = Animator.StringToHash("tRotation");

    public void PlayRotation()
    {
        animator.SetTrigger(TRotation);
    }    
}