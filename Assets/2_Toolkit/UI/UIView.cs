using UnityEngine;
using UnityEngine.UI;

namespace Toolkit.UI
{
    /// <summary>
    /// UIView included screen & popup
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    [RequireComponent(typeof(GraphicRaycaster))]
    public abstract class UIView : MonoBehaviour
    {
        public virtual string Key => GetType().FullName;

        protected Canvas _canvas;

        protected virtual void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvas.enabled = false;
        }

        public virtual void Init()
        {
            _canvas.enabled = false;
        }

        public virtual void Show()
        {
            _canvas.enabled = true;
        }

        public virtual void Hide()
        {
            _canvas.enabled = false;
        }

        public virtual void SetSortOrder(int order)
        {
            _canvas.sortingOrder = order;
        }
    }
}

