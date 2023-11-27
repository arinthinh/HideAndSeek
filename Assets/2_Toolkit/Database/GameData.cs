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
        isMusicOn = true;
        isSoundOn = true;
    }

    public int currentLevel;
    public int fruits;
    public List<int> skinOwned;
    public int currentSkin;
    public bool isMusicOn;
    public bool isSoundOn;
}