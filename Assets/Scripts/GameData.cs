using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Dictionary<string, int> LevelRanks = new Dictionary<string, int>();

    public GameData(EventManager eventManager)
    {

        Debug.Log($"Saved {eventManager} to file with value {eventManager}");
    }
}
