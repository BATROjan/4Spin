using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace UI.UIStartWindow
{
    public class UIStartWindow : UIWindow
    {
        public UIButton[] Buttons => buttons;
        [SerializeField] private UIButton[] buttons;

        public override void Show()
        {
        }

        public override void Hide()
        {
        }
    }
}
