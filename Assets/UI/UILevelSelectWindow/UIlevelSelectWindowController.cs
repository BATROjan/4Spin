using DG.Tweening;
using GameController;
using PlayingField;
using Tutorial;
using UI.UIPlayingWindow;
using UI.UIService;
using UnityEngine;

namespace UI.UILevelSelectWindow
{
    public class UIlevelSelectWindowController
    {
        private readonly TutorialFieldController _tutorialFieldController;
        private readonly PlayingFieldController _playingFieldController;
        private readonly GameConfig _gameConfig;
        private readonly GameController.GameController _gameController;
        private readonly IUIService _uiService;

        private UIlevelSelectWindow _uiSelectWindow;

        public UIlevelSelectWindowController(
            TutorialFieldController tutorialFieldController,
            PlayingFieldController playingFieldController,
            GameConfig gameConfig,
            GameController.GameController gameController,
            IUIService uiService)
        {
            _tutorialFieldController = tutorialFieldController;
            _playingFieldController = playingFieldController;
            _gameConfig = gameConfig;
            _gameController = gameController;
            _uiService = uiService;
            
            _uiSelectWindow = _uiService.Get<UIlevelSelectWindow>();
            _uiSelectWindow.ShowAction += Show;
            _uiSelectWindow.HideAction += UnSubscribeButtons;
        }

        private void Show()
        {
            PrepearWindow();
            InitButtons();
        }

        private void PrepearWindow()
        {
            ActivateGridsButton(true);
            ActivateSelectButtons(false);
        }

        public void InitButtons()
        {
            for (int i = 0; i < 3; i++)
            {
                _uiSelectWindow.Buttons[i].OnSelectLevel += ShowGrid;
            }
            _uiSelectWindow.Buttons[3].OnClick += BackToSelect;
            _uiSelectWindow.Buttons[4].OnClick += SelectGrid;
        }

        private void BackToSelect()
        {
            ActivateSelectButtons(false); 
            
            _tutorialFieldController.TutorialBaseFieldView.transform.DOScale(0, 1)
                .OnComplete(() =>
                {
                    _tutorialFieldController.DestroyField();
                    ActivateGridsButton(true);
                });
        }

        private void ShowGrid(DiffcultLevel diffcultLevel)
        {
            _gameConfig.DiffcultLevel = diffcultLevel;
            _tutorialFieldController.Spawn(diffcultLevel);
            ActivateGridsButton(false);
            
            _tutorialFieldController.TutorialBaseFieldView.transform.localScale = Vector3.zero;
            _tutorialFieldController.TutorialBaseFieldView.transform.DOScale(1, 1)
                .OnComplete(() => ActivateSelectButtons(true));
        }

        private void ActivateSelectButtons(bool value)
        {
            for (int i = 3; i < 5; i++)
            {
                _uiSelectWindow.Buttons[i].gameObject.SetActive(value);
            }
        }

        private void ActivateGridsButton(bool value)
        {
            for (int i = 0; i < 3; i++)
            {
                _uiSelectWindow.Buttons[i].gameObject.SetActive(value);
            }
        }

        private void SelectGrid()
        {
            _tutorialFieldController.DestroyField();
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
            for (int i = 0; i < 3; i++)
            {
                _uiSelectWindow.Buttons[i].OnSelectLevel -= ShowGrid;
            }
            _uiSelectWindow.Buttons[3].OnClick -= BackToSelect;
            _uiSelectWindow.Buttons[4].OnClick -= SelectGrid;
        }
    }
}