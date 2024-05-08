using Slider;
using UI.UIService;

namespace UI.UIPlayingWindow
{
    public class UIPlayingWindowController
    {
        private readonly SliderController _sliderController;
        private readonly IUIService _uiService;
        private UIPlayingWindowView _uiPlayingWindow;

        public UIPlayingWindowController(
            SliderController sliderController,
            IUIService uiService)
        {
            _sliderController = sliderController;
            _uiService = uiService;
            _uiPlayingWindow = _uiService.Get<UIPlayingWindowView>();
            _sliderController.SetSliderView(_uiPlayingWindow.SliderPanel);
        }

        public void ActivateSliderPanel(bool value)
        {
            _sliderController.ShowAnimation(value);
        }
    }
}