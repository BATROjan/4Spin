using UnityEngine;
using UnityEngine.UI;

namespace UI.UISelectOpponetWindow
{
    public class UISelectOpponentWindow : UIWindow
    {
        public OponentView[] Oponents => oponents;
        public UIButton[] Buttons => buttons;
        
        [SerializeField] private UIButton[] buttons;
        [SerializeField] private OponentView[] oponents;

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