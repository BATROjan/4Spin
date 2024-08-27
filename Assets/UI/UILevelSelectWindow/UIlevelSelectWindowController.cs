using GameController;
using UI.UIPlayingWindow;
using UI.UIService;

namespace UI.UILevelSelectWindow
{
    public class UIlevelSelectWindowController
    {
        private readonly GameConfig _gameConfig;
        private readonly GameController.GameController _gameController;
        private readonly IUIService _uiService;

        private UIlevelSelectWindow _uiSelectWindow;

        public UIlevelSelectWindowController(
            GameConfig gameConfig,
            GameController.GameController gameController,
            IUIService uiService)
        {
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
            _gameController.StartGame(diffcultLevel);

            _uiService.Hide<UIlevelSelectWindow>();
            _uiService.Show<UIPlayingWindowView>();
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