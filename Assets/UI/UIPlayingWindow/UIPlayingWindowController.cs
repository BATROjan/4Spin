using GameController;
using Grid;
using PlayingField;
using Slider;
using UI.UIService;

namespace UI.UIPlayingWindow
{
    public class UIPlayingWindowController
    {
        private readonly GameConfig _gameConfig;
        private readonly GridConfig _gridConfig;
        private readonly SliderController _sliderController;
        private readonly IUIService _uiService;
        private UIPlayingWindowView _uiPlayingWindow;

        public UIPlayingWindowController(
            GameConfig gameConfig,
            GridConfig gridConfig,
            SliderController sliderController,
            IUIService uiService)
        {
            _gameConfig = gameConfig;
            _gridConfig = gridConfig;
            _sliderController = sliderController;
            _uiService = uiService;
            _uiPlayingWindow = _uiService.Get<UIPlayingWindowView>();
            _sliderController.SetSliderView(_uiPlayingWindow.SliderPanel);
            
            _uiPlayingWindow.ShowAction += Show;
            _uiPlayingWindow.HideAction += Hide;
        }

        private void Hide()
        {
            UnSubscribeButtons();
        }

        private void Show()
        {
            _uiPlayingWindow.RulesText.text = 
                "Собери "+
                _gridConfig.GetGrid(_gameConfig.DiffcultLevel).CountCellsToWin + " монетки в ряд ";
            
            InitButtons();
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