using UI.UIService;
using UnityEngine.EventSystems;

namespace UI.UIStartWindow
{
    public class UIStartWindowController
    {
        private readonly GameController.GameController _gameController;
        private readonly IUIService _uiService;
        
        private UIButton[] _buttons;
        
        public UIStartWindowController(
            GameController.GameController gameController,
            IUIService uiService)
        {
            _gameController = gameController;
            _uiService = uiService;
            _buttons = _uiService.Get<UIStartWindow>().Buttons;
            InitButtons();
        }

        public void InitButtons()
        {
            _buttons[0].OnClick += StartGame;
        }

        private void StartGame()
        {
            _gameController.StartGame();
            _uiService.Hide<UIStartWindow>();
            UISubscribeButtons();
        }
        
        public void UISubscribeButtons()
        {
            _buttons[0].OnClick -= StartGame;
        }
    }
}