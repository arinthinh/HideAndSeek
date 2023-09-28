using UnityEngine;


public abstract class GameConfig : ScriptableObject
{
    public string ID => GetType().FullName;
}

public class ConfigIdDefine
{
    
}
