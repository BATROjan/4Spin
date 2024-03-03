using System;
using UI.UIService;
using UnityEngine;

namespace UI
{
    public abstract class UIWindow : MonoBehaviour, IUIWindow
    {
        public Action ShowAction { get; set; }
        public Action HideAction { get; set; }
        
        public IUIService UIService { get; set; }

        public abstract void Show();
        public abstract void Hide();
    }
}