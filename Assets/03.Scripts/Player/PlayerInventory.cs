using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<GameObject> _inventory = new List<GameObject>();

    void Awake()
    {
        _inventory.Clear();
    }

    public void AddItem(GameObject item)
    {
        _inventory.Add(item);
    }

    public void RemoveItem(GameObject item)
    {
        _inventory.Remove(item);
    }

    public GameObject GetItem(string ItemName)
    {
        return _inventory.Find(x => x.name == ItemName);
    }

    public GameObject GetLastItem()
    {
        return _inventory[^1];
    }

    public void PlayStoryEvent()
    {
        var paper = _inventory[^1].GetComponent<Paper>();

        paper.StartEvent();
    }
}