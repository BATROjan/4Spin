using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace UI.UISettingsWindow
{
    public class UISettingsWindowView : UIWindow
    {
        public UIButton SaveButton => _saveButton;
        public UnityEngine.UI.Slider[] Sliders => sliders;
        
        [SerializeField] private UnityEngine.UI.Slider[] sliders;
        [SerializeField] private UIButton _saveButton;

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