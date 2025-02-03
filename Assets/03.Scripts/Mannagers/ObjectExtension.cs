using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class ObjectExtension 
{
    private static List<Object> _savedObjects = new List<Object>();

    public static void Init()
    {
        _savedObjects.Clear();
    }
    
    public static void DontDestroyOnLoad(this Object obj)
    { 
        _savedObjects.Add(obj);
        Object.DontDestroyOnLoad(obj);
        
        for (int i = 0; i < obj.GameObject().transform.childCount; i++)
        {
            GameObject childobj = obj.GameObject().transform.GetChild(i).gameObject;

            _savedObjects.Add(childobj);
            // 자식 개체에는 실행하면 안된다.
            // Object.DontDestroyOnLoad(childobj);
        }
    }

    public static void Destroy(this Object obj)
    {
        _savedObjects.Remove(obj);
        Object.Destroy(obj);
    }

    public static List<Object> GetSavedObjects()
    {
        return _savedObjects;
    }

    public static void Log()
    {
        DebugManager.Instance?.Log("------Start Logging------");
        foreach (var obj in _savedObjects)
        {
            DebugManager.Instance?.Log($"{obj.name}");
        }
        DebugManager.Instance?.Log("-------End Logging-------");
    }

    public static Object FindObjectByID(string objectID)
    {
        return _savedObjects.Find(x => x.name == objectID);
    }
}
