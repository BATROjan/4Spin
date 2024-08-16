using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Slider
{
    public class SliderView : MonoBehaviour
    {
        public Image SliderImage => sliderImage;
        public UIButton[] UIButtons => uiButtons;
        public Vector3[] Positions => positions;
        public RectTransform RectTransforms => rectTransforms;
        
        [SerializeField] private Image sliderImage;
        [SerializeField] private UIButton[] uiButtons;
        [SerializeField] private Vector3[] positions;
        [SerializeField] private RectTransform rectTransforms;

    }
}