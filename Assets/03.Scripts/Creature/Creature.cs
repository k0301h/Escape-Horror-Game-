using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public int currentRegion;
    [SerializeField] protected Animator animator;
    
    [SerializeField] private bool lookPlayerState = false;
    [SerializeField] private GameObject _player;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (lookPlayerState)
        {
            transform.LookAt(_player.transform);
        }
    }

    public void LookAtPlayer()
    {
        lookPlayerState = true;

        _player = PlayerExtension.FindPlayerByID("Player").GameObject();
    }
}