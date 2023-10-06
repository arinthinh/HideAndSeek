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
        protected CanvasScaler _canvasScaler;
        

        protected void Awake()
        {
            _canvas = GetComponent<Canvas>();
            _canvasScaler = GetComponent<CanvasScaler>();
            _canvas.enabled = false;
        }

        public void Show()
        {
            _canvas.enabled = true;
        }

        public void Hide()
        {
            _canvas.enabled = false;
        }

        public void SetSortOrder(int order)
        {
            _canvas.sortingOrder = order;
        }
    }
}

