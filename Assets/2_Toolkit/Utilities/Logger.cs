using System;
using UnityEngine;
using Object = UnityEngine.Object;

public static class Logger
{
    private static string Color(this string myStr, string color)
    {
        return $"<color={color}>{myStr}</color>";
    }

    private static void DoLog(Action<string, UnityEngine.Object> LogFunction, string prefix, UnityEngine.Object myObj, params object[] msg)
    {
#if UNITY_EDITOR
        var name = (myObj ? myObj.name : "NullObject").Color("lightblue");
        LogFunction($"{prefix}[{name}]: {String.Join("; ", msg)}\n ", myObj);
#endif
    }

    public static void Log(this UnityEngine.Object myObj, params object[] msg)
    {
        DoLog(Debug.Log, "", myObj, msg);
    }

    public static void LogDetail(this UnityEngine.Object myObj, params object[] msg)
    {
        DoLog(Debug.Log, "➤".Color("blue"), myObj, msg);
    }

    public static void LogError(this UnityEngine.Object myObj, params object[] msg)
    {
        DoLog(Debug.LogError, "<!>".Color("red"), myObj, msg);
    }

    public static void LogWarning(this UnityEngine.Object myObj, params object[] msg)
    {
        DoLog(Debug.LogWarning, "⚠️".Color("yellow"), myObj, msg);
    }

    public static void LogSuccess(this UnityEngine.Object myObj, params object[] msg)
    {
        DoLog(Debug.Log, "☻".Color("green"), myObj, msg);
    }
}