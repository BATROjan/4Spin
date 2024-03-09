using UI.UIService;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UILevelSelectWindow
{
    public class UIlevelSelectWindow : UIWindow
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