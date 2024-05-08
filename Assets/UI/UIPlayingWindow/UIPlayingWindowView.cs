using Slider;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.UIPlayingWindow
{
    public class UIPlayingWindowView : UIWindow
    {
        public UIButton[] Buttons => buttons;
        public SliderView SliderPanel => sliderPanel;
        
        [SerializeField] private UIButton[] buttons; 
        [SerializeField] private SliderView sliderPanel;
        
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