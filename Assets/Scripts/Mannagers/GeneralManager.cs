using UnityEngine;

namespace Mannagers
{
    public class GeneralManager : MonoBehaviour
    {
        void Start()
        {
            ObjectExtension.Init();
            ObjectExtension.DontDestroyOnLoad(gameObject);
            // == gameObject.DontDestroyOnLoad(); // 확장 매서드 사용 예
            
            ObjectExtension.Log();
        }
    }
}