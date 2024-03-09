using System;
using System.Collections.Generic;
using MainCamera;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI.UIService
{
    public class UIService : IUIService
    {
        private Transform _deactivatedContainer;

        private readonly IUIRoot _uIRoot;
        private readonly Dictionary<Type, UIWindow> _viewStorage = new Dictionary<Type, UIWindow>();
        private readonly Dictionary<Type, GameObject> _initWindows = new Dictionary<Type, GameObject>();

        private const string UISource = "";

        public UIService(CameraController cameraController, IUIRoot uiRoot)
        {
            _uIRoot = uiRoot;

            LoadWindows(UISource);
            InitWindows(_uIRoot.DeativateContainer);
        }

        public T Show<T>() where T : UIWindow
        {
            var window = Get<T>();
            if (window != null)
            {
                window.transform.SetParent(_uIRoot.ActivateContainer, false);

                window.Show();
                return window;
            }
            return null;
        }

        public T Get<T>() where T : UIWindow
        {
            var type = typeof(T);
            if (_initWindows.ContainsKey(type))
            {
                var view = _initWindows[type];
                return view.GetComponent<T>();
            }
            return null;
        }

        public void Hide<T>(Action onEnd = null) where T : UIWindow
        {
            var window = Get<T>();

            if (window != null)
            {
                window.transform.SetParent(_uIRoot.DeativateContainer);
                window.Hide();

                onEnd?.Invoke();
            }
        }

        public void InitWindows(Transform poolDeactiveContainer)
        {
            _deactivatedContainer = poolDeactiveContainer == null ? _uIRoot.DeativateContainer : poolDeactiveContainer;
            foreach (var windowKVP in _viewStorage)
            {
                Init(windowKVP.Key, _deactivatedContainer);
            }
        }

        public void LoadWindows(string source)
        {
            var windows = Resources.LoadAll(source, typeof(UIWindow));

            foreach (var window in windows)
            {
                var windowType = window.GetType();

                _viewStorage.Add(windowType, (UIWindow)window);
            }
        }

        private void Init(Type t, Transform parent = null)
        {
            if (_viewStorage.ContainsKey(t))
            {
                GameObject view = null;
                if (parent != null)
                {
                    view = Object.Instantiate(_viewStorage[t].gameObject, parent);
                }
                else
                {
                    view = Object.Instantiate(_viewStorage[t].gameObject);
                }

                var uiWindow = view.GetComponent<UIWindow>();
                uiWindow.UIService = this;

                _initWindows.Add(t, view);
            }
        }
    }
}
