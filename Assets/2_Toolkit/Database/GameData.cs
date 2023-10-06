using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public GameData()
    {
        currentLevel = 1;
        fruits = 0;
        skinOwned = new() { 0 };
    }

    public int currentLevel;
    public int fruits;
    public List<int> skinOwned;
}



