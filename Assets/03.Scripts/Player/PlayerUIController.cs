using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct UI_Index
{
    public static readonly int FurnitureID = 1;
    public static readonly int CursorID = 2;
    public static readonly int LockID = 3;
    public static readonly int ItemID = 4;
    public static readonly int StoryBackID = 5;
    public static readonly int StoryLineID = 6;
}

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private List<GameObject> UIElements = new List<GameObject>();
    
    // [SerializeField] private GameObject _FurnitureImage;
    // [SerializeField] private GameObject _CursorImage;
    // [SerializeField] private GameObject _LockImage;
    // [SerializeField] private GameObject _ItemImage;
    //
    // [SerializeField] private GameObject StoryBackGroundImage;
    // [SerializeField] private GameObject StoryLineImage;

    void Start()
    {
        UIElements.Clear();
        
        var image = GetComponentsInChildren<RawImage>();
        
        UIElements.Add(image[0].gameObject);
        UIElements.Add(image[1].transform.parent.gameObject);
        UIElements.Add(image[1].gameObject);
        UIElements.Add(image[2].gameObject);
        UIElements.Add(image[4].transform.parent.gameObject);
        UIElements.Add(image[6].gameObject);
        
        var text = GetComponentsInChildren<TextMeshProUGUI>();

        UIElements.Add(text[0].gameObject);

        foreach (var UIElem in UIElements)
        {
            UIElem.SetActive(false);
        }
        
        UIElements[0].SetActive(true);
        // _FurnitureImage.SetActive(false);
        // _CursorImage.SetActive(false);
        // _LockImage.SetActive(false);
        // _ItemImage.SetActive(false);
        // StoryBackGroundImage.SetActive(false);
        // StoryLineImage.SetActive(false);
    }

    public void SetUI(int id, bool isActive)
    {
        UIElements[id].SetActive(isActive);
    }
}