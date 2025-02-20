using UnityEngine;

public class Creature : MonoBehaviour
{
    public int currentRegion;
    [SerializeField] protected Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }
}