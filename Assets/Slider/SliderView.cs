using UI;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Slider
{
    public class SliderView : MonoBehaviour
    {
        public Image SliderArrow => sliderArrow;
        public Image InvisSliderImage => invisSliderImage;
        public UIButton[] UIButtons => uiButtons;
        public Vector3[] Positions => positions;
        public RectTransform RectTransforms => rectTransforms;
        
        [SerializeField] private Image sliderArrow;
        [SerializeField] private Image invisSliderImage;
        [SerializeField] private UIButton[] uiButtons;
        [SerializeField] private Vector3[] positions;
        [SerializeField] private RectTransform rectTransforms;

    }
}