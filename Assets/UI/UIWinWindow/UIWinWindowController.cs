using UI.UIService;

namespace UI.UIWinWindow
{
    public class UIWinWindowController
    {
        private readonly GameController.GameController _gameController;
        private readonly IUIService _uiService;

        private UIWinWindowView _windowView;
        
        UIWinWindowController(
            GameController.GameController gameController,
            IUIService uiService )
        {
            _gameController = gameController;
            _uiService = uiService;
            _windowView = _uiService.Get<UIWinWindowView>();
            _windowView.ShowAction += ShowLogic;
            _windowView.HideAction += HideLogic;
        }

        public UIWinWindowView GetUIWinWindowView()
        {
            return _windowView;
        }

        private void HideLogic()
        {
            UnSubscribeButton();
        }

        private void UnSubscribeButton()
        {
            _windowView.UIButtons[0].OnClick -= _gameController.RestartGame;
            _windowView.UIButtons[0].OnClick -= HideLogic;
        }

        private void ShowLogic()
        {
            InitButton();
        }

        private void InitButton()
        {
            _windowView.UIButtons[0].OnClick += _gameController.RestartGame;
            _windowView.UIButtons[0].OnClick += HideLogic;
        }
        
    }
}