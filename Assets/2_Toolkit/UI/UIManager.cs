using System.Collections.Generic;
using Toolkit.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private UIView[] _viewsArr;
    private Dictionary<string, UIView> _viewsDic = new();

    public void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        foreach (var view in _viewsArr)
        {
            _viewsDic.Add(view.Key, view);
        }
    }

    public UIView GetView<T>()
    {
        var key = typeof(T).FullName;
        return _viewsDic.ContainsKey(key) ? _viewsDic[key] : null;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _viewsArr = gameObject.GetComponentsInChildren<UIView>();
    }
#endif
}