using UnityEngine;
using UnityEngine.UI;

namespace UI.UIWinWindow
{
    public class UIWinWindowView : UIWindow
    {
        public UIButton[] UIButtons => uiButtons;

        public Text WinText;
        
       // [SerializeField] private Text winText;
        [SerializeField] private UIButton[] uiButtons;
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