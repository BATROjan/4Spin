using UI.UILevelSelectWindow;
using UI.UIService;
using UnityEngine;

namespace UI.UISelectOpponetWindow
{
    public class UISelectOpponentWindowController
    {
        private readonly IUIService _uiService;

        private UISelectOpponentWindow _uiSelectOpponentWindow;

        public UISelectOpponentWindowController(
            IUIService uiService)
        {
            _uiService = uiService;
            _uiSelectOpponentWindow = _uiService.Get<UISelectOpponentWindow>();

            _uiSelectOpponentWindow.ShowAction += InitButtons;
        }

        private void InitButtons()
        {
            _uiSelectOpponentWindow.Buttons[0].OnClick += SelectPlayer;
            _uiSelectOpponentWindow.Buttons[1].OnClick += SelectComputer;
        }
        private void SelectOpponent()
        {
            _uiService.Hide<UISelectOpponentWindow>();
            _uiService.Show<UIlevelSelectWindow>();
            
            UISubscribeButtons();
        }

        private void UISubscribeButtons()
        {
            _uiSelectOpponentWindow.Buttons[0].OnClick -= SelectPlayer;
            _uiSelectOpponentWindow.Buttons[1].OnClick -= SelectComputer;
        }

        private void SelectComputer()
        {
            SelectOpponent();
        }

        private void SelectPlayer()
        {
            SelectOpponent();
        }
    }
}