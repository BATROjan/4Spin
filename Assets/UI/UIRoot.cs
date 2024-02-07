using UnityEngine;

namespace UI
{
    public class UIRoot : MonoBehaviour, IUIRoot
    {
        public Canvas Canvas
        {
            get => canvas;
            set => canvas = value;
        }

        public UIWindow[] PoolWindows => PoolWindows;

        [SerializeField] private Canvas canvas;
        [SerializeField] private UIWindow[] poolWindows;
    }
}