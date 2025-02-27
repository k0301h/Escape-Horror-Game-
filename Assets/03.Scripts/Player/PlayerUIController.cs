using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct UI_Index
{
    public static readonly int MosaicID = 0;
    public static readonly int AimID = 1;
    public static readonly int FurnitureID = 2;
    public static readonly int CursorID = 3;
    public static readonly int LockID = 4;
    public static readonly int ItemID = 5;
    public static readonly int StoryBackID = 6;
    public static readonly int StoryLineID = 7;
    public static readonly int StoryExitButtonID = 8;
}

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private List<GameObject> UIElements = new List<GameObject>();
    [SerializeField] private MosaicScript _mosaicScript;
    
    private Coroutine _storyLineCoroutine;

    void Start()
    {
        _mosaicScript = GetComponentInChildren<MosaicScript>();
        
        UIElements.Clear();
        
        var image = GetComponentsInChildren<RawImage>();
        var text = GetComponentsInChildren<TextMeshProUGUI>();
        var buttons = GetComponentsInChildren<Button>();
        
        UIElements.Add(image[0].gameObject); // Mosaic
        UIElements.Add(image[1].gameObject); // Aim
        UIElements.Add(image[2].transform.parent.gameObject); // Furniture_Cursor Parent
        UIElements.Add(image[2].gameObject); // Furniture_Cursor
        UIElements.Add(image[3].gameObject); // Lock_Cursor
        UIElements.Add(image[5].transform.parent.gameObject); // Item_Cursor Parent
        UIElements.Add(image[7].gameObject); // Story_Back

        UIElements.Add(text[0].gameObject); // Story_Line
        
        UIElements.Add(buttons[0].gameObject); // Story_Exit_Button

        foreach (var UIElem in UIElements)
        {
            UIElem.SetActive(false);
        }
        
        UIElements[1].SetActive(true);

        _storyLineCoroutine = null;
    }

    public GameObject GetUIGameObject(int id)
    {
        return UIElements[id];
    }

    public void SetUI(int id, bool isActive)
    {
        UIElements[id].SetActive(isActive);
    }
    
    public void SetStoryLine(string line)
    {
        var text = UIElements[UI_Index.StoryLineID].GetComponent<TextMeshProUGUI>();

        text.text = line;
    }

    public void MosaicPlayer(bool isOn)
    {
        if (isOn)
        {
            UIElements[UI_Index.MosaicID].SetActive(true);
            _mosaicScript.StartMosaic();
        }
        else
        {
            UIElements[UI_Index.MosaicID].SetActive(false);
            _mosaicScript.StopMosaic();
        }
    }

    public void UpdateMosaic()
    {
        _mosaicScript.UpdateMosaic();
    }

    public void MosaicColorSet(Color color)
    {
        UIElements[UI_Index.MosaicID].GetComponent<RawImage>().color = color;
    }
}