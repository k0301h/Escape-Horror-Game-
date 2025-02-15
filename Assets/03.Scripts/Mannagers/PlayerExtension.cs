using System.Collections.Generic;
using UnityEngine;

public static class PlayerExtension
{
    private static List<Object> _savedPlayers = new List<Object>();

    public static void Init()
    {
        _savedPlayers.Clear();
    }

    public static void AddPlayer(this Object player)
    {
        _savedPlayers.Add(player);
        DebugManager.Instance.Log($"{_savedPlayers.Count}, {_savedPlayers[0]}");
    }
    
    public static void RemovePlayer(this Object player)
    {
        _savedPlayers.Remove(player);
    }
    
    public static Object FindPlayerByID(string playerID)
    {
        DebugManager.Instance.Log($"{_savedPlayers.Count}, {_savedPlayers[0]}");
        
        // TODO: 이 함수를 호출하면 갑자기 List가 0으로 초기화되는 현상 해결해야함
        return _savedPlayers.Find(x => x.name == playerID);
    }
}