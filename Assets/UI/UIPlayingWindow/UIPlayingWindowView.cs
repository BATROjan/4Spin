using UnityEngine;
using UnityEngine.Serialization;

namespace UI.UIPlayingWindow
{
    public class UIPlayingWindowView : UIWindow
    {
        public UIButton[] Buttons => buttons;
        public Vector3[] Positions => positions;
        public Transform SliderPanel => sliderPanel;
        
        [SerializeField] private UIButton[] buttons;
        [SerializeField] private Vector3[] positions;
        [SerializeField] private Transform sliderPanel;
        
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