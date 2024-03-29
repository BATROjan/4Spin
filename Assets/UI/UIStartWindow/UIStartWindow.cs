using DG.Tweening;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace UI.UIStartWindow
{
    public class UIStartWindow : UIWindow
    {
        public UIButton[] Buttons => buttons;
        
        [SerializeField] private UIButton[] buttons;
        [SerializeField] private Image traingleImage;

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
