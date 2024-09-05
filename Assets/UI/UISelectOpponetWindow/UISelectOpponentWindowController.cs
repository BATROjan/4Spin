using PlayingField;
using UI.UILevelSelectWindow;
using UI.UIService;
using UnityEngine;

namespace UI.UISelectOpponetWindow
{
    public class UISelectOpponentWindowController
    {
        private readonly PlayingFieldController _playingFieldController;
        private readonly GameController.GameController _gameController;
        private readonly IUIService _uiService;

        private UISelectOpponentWindow _uiSelectOpponentWindow;

        public UISelectOpponentWindowController(
            PlayingFieldController playingFieldController,
            GameController.GameController gameController,
            IUIService uiService)
        {
            _playingFieldController = playingFieldController;
            _gameController = gameController;
            _uiService = uiService;
            _uiSelectOpponentWindow = _uiService.Get<UISelectOpponentWindow>();

            _uiSelectOpponentWindow.ShowAction += InitButtons;
            _uiSelectOpponentWindow.HideAction += Hide;
        }

        private void Hide()
        {
            _playingFieldController.ActivateBlackBackground(true);
        }

        private void InitButtons()
        {
            _uiSelectOpponentWindow.Buttons[0].OnClick += SelectPlayer;
            _uiSelectOpponentWindow.Buttons[1].OnClick += SelectComputer;
        }
        private void SelectOpponent()
        {
            _uiService.Hide<UISelectOpponentWindow>();
            _playingFieldController.ChangeBackSpritePosition(BackSpriteType.SelectGridWindow);
            _playingFieldController.OnAnimationEnd += ShowWindow;
        }

        private void ShowWindow()
        {
            _uiService.Show<UIlevelSelectWindow>();
            UISubscribeButtons();
            _playingFieldController.OnAnimationEnd -= ShowWindow;
        }

        private void UISubscribeButtons()
        {
            _uiSelectOpponentWindow.Buttons[0].OnClick -= SelectPlayer;
            _uiSelectOpponentWindow.Buttons[1].OnClick -= SelectComputer;
        }

        private void SelectComputer()
        {
            SelectOpponent();
            _gameController.SetPvE(false);
        }

        private void SelectPlayer()
        {
            SelectOpponent();
            _gameController.SetPvE(true);
        }
    }
}