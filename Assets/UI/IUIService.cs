using System;
using UnityEngine;
namespace UI.UIService
{
    public interface IUIService
    {
        T Show<T>() where T : UIWindow;
        void Hide<T>(Action onEnd = null) where T : UIWindow;
        T Get<T>() where T : UIWindow;
        void InitWindows(Transform poolDeactiveContiner);
        void LoadWindows(string source);
    }
}