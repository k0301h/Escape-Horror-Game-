using System;
using UnityEngine;

public class Region_Collider : MonoBehaviour
{
    [SerializeField] private int _regionType;
    [SerializeField] private PlayerInfo _player;
    public BoxCollider boxCollider; 

    private void Start()
    {
        RegionExtension.AddRegion(this, _regionType);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _player.CurrentRegion = _regionType;
        }
        else if (other.CompareTag("Creature"))
        {
            var creature = other.GetComponent<Creature>();
            creature.currentRegion = _regionType;
        }
    }
}
