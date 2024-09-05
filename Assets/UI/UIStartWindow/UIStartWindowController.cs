using PlayingField;
using UI.UILevelSelectWindow;
using UI.UISelectOpponetWindow;
using UI.UIService;
using UnityEngine.EventSystems;

namespace UI.UIStartWindow
{
    public class UIStartWindowController
    {
        private readonly PlayingFieldController _playingFieldController;
        private readonly AudioController.AudioController _audioController;
        private readonly IUIService _uiService;

        private UIStartWindow _uiStartWindow;

        public UIStartWindowController(
            PlayingFieldController playingFieldController,
            AudioController.AudioController audioController,
            IUIService uiService)
        {
            _playingFieldController = playingFieldController;
            _audioController = audioController;
            _uiService = uiService;
            _uiStartWindow = _uiService.Get<UIStartWindow>();
            
            _uiStartWindow.ShowAction += Show;
            _uiStartWindow.HideAction += UnSubscribeButtons;

            _playingFieldController.SpawnBackSpriteView();
            _uiService.Show<UIStartWindow>();
            _audioController.Play(AudioType.Background, 1, true);
        }

        private void Show()
        {
            _playingFieldController.ActivateBlackBackground(false);
            InitButtons();
        }

        public void InitButtons()
        {
            _uiStartWindow.Buttons[0].OnClick += HideWindowAnimation;
            _uiStartWindow.Buttons[1].OnClick += StartTutorial;
        }

        private void StartTutorial()
        {
            _uiService.Hide<UIStartWindow>();
            _uiService.Show<UITutorialWindow.UITutorialWindow>();
        }

        private void HideWindowAnimation()
        { 
            _uiService.Hide<UIStartWindow>();
            _playingFieldController.ChangeBackSpritePosition(BackSpriteType.SelectOpponentWindow);
            _playingFieldController.OnAnimationEnd += SelectLevel;
        }

        private void SelectLevel()
        {
            _uiService.Show<UISelectOpponentWindow>();
           _playingFieldController.OnAnimationEnd -= SelectLevel;
        }

        public void UnSubscribeButtons()
        {
            _uiStartWindow.Buttons[0].OnClick -= HideWindowAnimation;
            _uiStartWindow.Buttons[1].OnClick -= StartTutorial;
        }
    }
}