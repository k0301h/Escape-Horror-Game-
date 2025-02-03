using System.Collections;
using TMPro;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    private static LineManager _instance;
    
    private Canvas _canvas;
    private TextMeshProUGUI _lineText;
    private TextMeshProUGUI _mapText;
    
    private Coroutine _lineCoroutine;
    private Coroutine _MapCoroutine;

    void Awake()
    {
        if(_instance == null)
            _instance = this;

        _canvas = GetComponentInChildren<Canvas>();        
        var texts = GetComponentsInChildren<TextMeshProUGUI>();
        
        _lineText = texts[0];
        _mapText = texts[1];
    }

    public static LineManager Instance
    {
        get
        {
            if(_instance == null)
                return null;
            
            return _instance;
        }
    }

    IEnumerator EraseLineCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        
        _lineText.text = null;
        _lineCoroutine = null;
    }
    
    public void ShowLine(string line)
    {
        // GameObject line_text = Instantiate(_lineText.gameObject, _canvas.transform);
        // line_text.GetComponent<TextMeshProUGUI>().text = line;
        // Destroy(line_text, 3.0f);
        
        if(_lineCoroutine != null)
            StopCoroutine(_lineCoroutine);
        
        _lineText.text = line;
        _lineCoroutine = StartCoroutine(EraseLineCoroutine());
    }

    IEnumerator EraseMapCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        
        _mapText.text = null;
        _MapCoroutine = null;
    }
    
    public void ShowMapName(string mapName)
    {
        // GameObject map_text = Instantiate(_mapText.gameObject, _canvas.transform);
        // map_text.GetComponent<TextMeshProUGUI>().text = mapName;
        // Destroy(map_text, 3.0f);
        
        
        if(_MapCoroutine != null)
            StopCoroutine(_MapCoroutine);
        
        _mapText.text = mapName;
        _MapCoroutine = StartCoroutine(EraseMapCoroutine());
        
    }
}
