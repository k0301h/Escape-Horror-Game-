using UnityEngine;

public class Creature : MonoBehaviour
{
    [SerializeField] protected Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }
}