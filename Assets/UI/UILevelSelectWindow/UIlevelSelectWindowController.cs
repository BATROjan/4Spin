using GameController;
using PlayingField;
using UI.UIPlayingWindow;
using UI.UIService;

namespace UI.UILevelSelectWindow
{
    public class UIlevelSelectWindowController
    {
        private readonly PlayingFieldController _playingFieldController;
        private readonly GameConfig _gameConfig;
        private readonly GameController.GameController _gameController;
        private readonly IUIService _uiService;

        private UIlevelSelectWindow _uiSelectWindow;

        public UIlevelSelectWindowController(
            PlayingFieldController playingFieldController,
            GameConfig gameConfig,
            GameController.GameController gameController,
            IUIService uiService)
        {
            _playingFieldController = playingFieldController;
            _gameConfig = gameConfig;
            _gameController = gameController;
            _uiService = uiService;
            
            _uiSelectWindow = _uiService.Get<UIlevelSelectWindow>();
            _uiSelectWindow.ShowAction += InitButtons;
            _uiSelectWindow.HideAction += UnSubscribeButtons;
        }
        public void InitButtons()
        {
            foreach (var button in _uiSelectWindow.Buttons)
            {
                button.OnSelectLevel += SelectLevel;
            }
        }

        private void SelectLevel(DiffcultLevel diffcultLevel)
        {
            _gameConfig.DiffcultLevel = diffcultLevel;
            _uiService.Hide<UIlevelSelectWindow>();
            
            _playingFieldController.ChangeBackSpritePosition(BackSpriteType.StartWindow);
            _playingFieldController.OnAnimationEnd += ShowWindow;
        }

        private void ShowWindow()
        {
            _gameController.StartGame(_gameConfig.DiffcultLevel);
            _uiService.Show<UIPlayingWindowView>();
            _playingFieldController.OnAnimationEnd -= ShowWindow;
        }

        public void UnSubscribeButtons()
        {
            foreach (var button in _uiSelectWindow.Buttons)
            {
                button.OnSelectLevel -= SelectLevel;
            }
        }
    }
}