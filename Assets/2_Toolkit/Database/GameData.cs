using System;
using UnityEngine;

[Serializable]
public class GameData
{
    public GameData()
    {
        UserInfo = new();
    }
    
    public UserInfoData UserInfo;
}

[Serializable]
public class UserInfoData
{
    public string PlayerID;
}


