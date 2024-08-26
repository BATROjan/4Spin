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
            
            _uiPlayingWindow.ShowAction += InitButtons;
            _uiPlayingWindow.HideAction += UnSubscribeButtons;
        }

        private void UnSubscribeButtons()
        {
            _uiPlayingWindow.Buttons[0].OnClick -= ShowPauseWindow;
        }

        private void InitButtons()
        {
            _uiPlayingWindow.Buttons[0].OnClick += ShowPauseWindow;
        }

        private void ShowPauseWindow()
        {
            _uiService.Hide<UIPlayingWindowView>();
            _uiService.Show<UIPauseGameWindow.UIPauseGameWindow>();
        }
    }
}