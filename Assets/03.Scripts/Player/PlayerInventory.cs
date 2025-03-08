using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<GameObject> _inventory = new List<GameObject>();

    void Awake()
    {
        // TODO : 테스트용 주석처리
        // DebugManager.Instance.Log("Player Inventory 테스트용 주석처리");
        // _inventory.Clear();
    }

    public void AddItem(GameObject item)
    {
        _inventory.Add(item);
    }

    public void RemoveItem(GameObject item)
    {
        _inventory.Remove(item);
    }

    public GameObject GetItem(string itemName)
    {
        return _inventory.Find(x => x.name == itemName);
    }

    public GameObject GetLastItem()
    {
        return _inventory[^1];
    }

    public void PlayStoryEvent()
    {
        var paper = _inventory[^1].GetComponent<EventScript>();

        paper.StartPaperEvent();
    }
}