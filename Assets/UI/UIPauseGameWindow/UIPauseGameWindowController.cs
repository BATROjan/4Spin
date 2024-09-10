using DG.Tweening;
using UI.UILevelSelectWindow;
using UI.UIPlayingWindow;
using UI.UIService;

namespace UI.UIPauseGameWindow
{
    public class UIPauseGameWindowController
    {
        private readonly AudioController.AudioController _audioController;
        private readonly GameController.GameController _gameController;
        private readonly IUIService _uiService;

        private UIPauseGameWindow _uiPauseGameWindow;
        
        public UIPauseGameWindowController(
            AudioController.AudioController audioController,
            GameController.GameController gameController,
            IUIService uiService)
        {
            _audioController = audioController;
            _gameController = gameController;
            _uiService = uiService;
            
            _uiPauseGameWindow = _uiService.Get<UIPauseGameWindow>();
            _uiPauseGameWindow.ShowAction += Show;
            _uiPauseGameWindow.HideAction += Hide;
        }

        private void Show()
        {
            _audioController.ActivateMuteEffect(true);
            _gameController.TimeControll(false);
            InitButtons();
        }

        private void Hide()
        {
            _audioController.ActivateMuteEffect(false);
            _gameController.TimeControll(true);
            
            UnSubscribeButtons();
        }

        private void InitButtons()
        {
            _uiPauseGameWindow.Buttons[0].OnClick += ReturnToMenu;
            _uiPauseGameWindow.Buttons[1].OnClick += ResumeGame;
        }

        private void ShowSettings()
        {
            _uiService.Show<UISettingsWindow.UISettingsWindowView>();
            _uiService.Hide<UIPauseGameWindow>();
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
            _uiPauseGameWindow.Buttons[0].OnClick -= ReturnToMenu;
            _uiPauseGameWindow.Buttons[1].OnClick -= ResumeGame;
        }
    }
}