using UnityEngine;

namespace Mannagers
{
    public class GeneralManager : MonoBehaviour
    {
        void Awake()
        {
            ObjectExtension.Init();
            ObjectExtension.DontDestroyOnLoad(this.gameObject);
            // == gameObject.DontDestroyOnLoad(); // 확장 매서드 사용 예
            
            ObjectExtension.Log();
            
            PlayerExtension.Init();
        }
    }
}