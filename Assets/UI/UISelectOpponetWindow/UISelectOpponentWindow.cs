using UnityEngine;
using UnityEngine.UI;

namespace UI.UISelectOpponetWindow
{
    public class UISelectOpponentWindow : UIWindow
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