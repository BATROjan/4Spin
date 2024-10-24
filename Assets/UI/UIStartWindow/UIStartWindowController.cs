using PlayingField;
using Tutorial;
using UI.UILevelSelectWindow;
using UI.UISelectOpponetWindow;
using UI.UIService;
using UI.UISettingsWindow;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.UIStartWindow
{
    public class UIStartWindowController
    {
        private readonly TutorialFieldController _tutorialFieldController;
        private readonly PlayingFieldController _playingFieldController;
        private readonly AudioController.AudioController _audioController;
        private readonly IUIService _uiService;

        private UIStartWindow _uiStartWindow;

        public UIStartWindowController(
            TutorialFieldController tutorialFieldController,
            PlayingFieldController playingFieldController,
            AudioController.AudioController audioController,
            IUIService uiService)
        {
            _tutorialFieldController = tutorialFieldController;
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
            _tutorialFieldController.Spawn(DiffcultLevel.Normal);
            InitButtons();
        }

        public void InitButtons()
        {
            _uiStartWindow.Buttons[0].OnClick += HideWindowAnimation;
            _uiStartWindow.Buttons[1].OnClick += StartTutorial;
            _uiStartWindow.Buttons[2].OnClick += OpenSettings;
            _uiStartWindow.Buttons[3].OnClick += Exit;
        }

        private void Exit()
        {
            Application.Quit();
        }

        private void OpenSettings()
        {
            _uiService.Hide<UIStartWindow>();
            _tutorialFieldController.DestroyField();
            _playingFieldController.ActivateBlackBackground(false);
            _playingFieldController.ChangeBackSpritePosition(BackSpriteType.Setting);
            _playingFieldController.OnAnimationEnd += ShowSettings;
        }

        private void ShowSettings()
        {
            _uiService.Show<UISettingsWindowView>();
            _playingFieldController.OnAnimationEnd -= ShowSettings;
        }

        private void StartTutorial()
        {
            _uiService.Hide<UIStartWindow>();
            _tutorialFieldController.DestroyField();
            _playingFieldController.ActivateBlackBackground(true);
            _playingFieldController.ChangeBackSpritePosition(BackSpriteType.Rules);
            _playingFieldController.OnAnimationEnd += ShowTutor;
        }

        private void ShowTutor()
        {
            _uiService.Show<UITutorialWindow.UITutorialWindow>();
            _playingFieldController.OnAnimationEnd -= ShowTutor;
        }

        private void HideWindowAnimation()
        { 
            _uiService.Hide<UIStartWindow>();
            _tutorialFieldController.DestroyField();
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
            _uiStartWindow.Buttons[2].OnClick -= OpenSettings;
            _uiStartWindow.Buttons[3].OnClick -= Exit;
        }
    }
}