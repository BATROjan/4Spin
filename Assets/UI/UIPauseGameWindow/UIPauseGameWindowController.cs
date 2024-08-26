using UI.UILevelSelectWindow;
using UI.UIPlayingWindow;
using UI.UIService;

namespace UI.UIPauseGameWindow
{
    public class UIPauseGameWindowController
    {
        private readonly GameController.GameController _gameController;
        private readonly IUIService _uiService;

        private UIPauseGameWindow _uiPauseGameWindow;
        
        public UIPauseGameWindowController(
            GameController.GameController gameController,
            IUIService uiService)
        {
            _gameController = gameController;
            _uiService = uiService;
            
            _uiPauseGameWindow = _uiService.Get<UIPauseGameWindow>();
            _uiPauseGameWindow.ShowAction += InitButtons;
            _uiPauseGameWindow.HideAction += UnSubscribeButtons;
        }

        private void InitButtons()
        {
            _gameController.TimeControll(false);
            _uiPauseGameWindow.Buttons[0].OnClick += ReturnToMenu;
            _uiPauseGameWindow.Buttons[1].OnClick += ResumeGame;
        }

        private void ResumeGame()
        {
            _uiService.Show<UIPlayingWindowView>();
            _uiService.Hide<UIPauseGameWindow>();
        }

        private void ReturnToMenu()
        {
            _uiService.Show<UIStartWindow.UIStartWindow>();
            _uiService.Hide<UIPauseGameWindow>();
           _gameController.ResetGame();
        }

        private void UnSubscribeButtons()
        {
            _gameController.TimeControll(true);
            _uiPauseGameWindow.Buttons[0].OnClick -= ReturnToMenu;
            _uiPauseGameWindow.Buttons[1].OnClick -= ResumeGame;
        }
    }
}