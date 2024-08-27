using UnityEngine;
using UnityEngine.UI;

namespace UI.UITutorialWindow
{
    public class UITutorialWindow : UIWindow
    {
        public UIButton[] Buttons => buttons;
        public Image Image => image;
        public Text Text => text;
        
        [SerializeField] private UIButton[] buttons;
        [SerializeField] private Image image;
        [SerializeField] private Text text;
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