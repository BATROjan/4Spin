using UnityEngine;

namespace UI.UIPauseGameWindow
{
    public class UIPauseGameWindow : UIWindow
    {
        public UIButton[] Buttons => buttons;
        
        [SerializeField] private UIButton[] buttons;
        public override void Show()
        {
            ShowAction?.Invoke();
        }

        public override void Hide()
        {
            HideAction?.Invoke();
        }
    }
}