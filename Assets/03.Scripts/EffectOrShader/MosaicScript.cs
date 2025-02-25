using UnityEngine;
using UnityEngine.UI;

public class MosaicScript : MonoBehaviour
{
    private int _width, _height;
    private double aspect;
    
    [SerializeField] private int strength = 10;
    
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private RenderTexture _mosaicTexture;
    
    [SerializeField] private RawImage _mosaicUI;
    [SerializeField] private RectTransform _mosaicTransform;

    // Render Texture 종횡비를 바탕으로 UI의 종횡비도 설정해야한다.
    
    void Start()
    {
        UpdateMosaic();
    }

    public void StartMosaic()
    {
        _playerCamera.targetTexture = _mosaicTexture;
        _mosaicUI.enabled = true;
    }

    public void UpdateMosaic()
    {
        _width = Screen.width;
        _height = Screen.height;
        
        _mosaicTexture.Release();
        
        aspect = _width / (double)_height;
        
        _width = (int)(_width * aspect);
        
        _mosaicTexture.width = _width / strength;
        _mosaicTexture.height = _height / strength;
        
        _mosaicTransform.sizeDelta = new Vector2(_width, _height);
        
        _mosaicTexture.Create();
    }
    
    public void StopMosaic()
    {
        _playerCamera.targetTexture = null;
        _mosaicUI.enabled = false;
    }
}
