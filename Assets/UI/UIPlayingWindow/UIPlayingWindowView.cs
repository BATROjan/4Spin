using Slider;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.UIPlayingWindow
{
    public class UIPlayingWindowView : UIWindow
    {
        public UIButton[] Buttons => buttons;
        public SliderView SliderPanel => sliderPanel;
        public Text RulesText => rulesText;
        
        [SerializeField] private UIButton[] buttons; 
        [SerializeField] private SliderView sliderPanel;
        [SerializeField] private Text rulesText;
        
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