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
    }
    
    public static void RemovePlayer(this Object player)
    {
        _savedPlayers.Remove(player);
    }
    
    public static Object FindPlayerByID(string playerID)
    {
        return _savedPlayers.Find(x => x.name == playerID);
    }
}