using System;
using UnityEngine;

public class PlayerIKController : MonoBehaviour
{ 
    public Animator animator;

    public bool isIKActive;
    public Transform rightHandPosition;

    void Start()
    {
        isIKActive = false;
        animator = GetComponent<Animator>();
    }

    // 추후에 다른 아이템이 생긴다면 여러 불린을 만들어서 관리하자
    public void changeIK()
    {
        isIKActive = !isIKActive;
    }
    
    void OnAnimatorIK()
    {
        if (animator)
        {
            if (isIKActive)
            {
                if (rightHandPosition != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandPosition.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandPosition.rotation);
                }
            }
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            }
        }
    }
}