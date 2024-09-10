using DG.Tweening;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace UI.UIStartWindow
{
    public class UIStartWindow : UIWindow
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
