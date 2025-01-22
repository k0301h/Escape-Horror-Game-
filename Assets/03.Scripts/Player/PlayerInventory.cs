using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> _inventory = new List<GameObject>();

    void Awake()
    {
        _inventory.Clear();
    }

    public void GetItem(GameObject item)
    {
        _inventory.Add(item);
    }

    public void RemoveItem(GameObject item)
    {
        _inventory.Remove(item);
    }
}