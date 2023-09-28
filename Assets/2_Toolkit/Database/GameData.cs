using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
    public GameData()
    {
        UserInfo = new();
    }
    
    public UserInfoData UserInfo;
    
    public int currentLevel;
    public List<int> characterSkinOwned;
    public List<int> grimaceSkinOwned;
    public bool isRemovedAds;

    public int coins;
}

[Serializable]
public class UserInfoData
{
    public string playerID;
    
}


