using UI.UIService;

namespace UI.UILevelSelectWindow
{
    public class UIlevelSelectWindowController
    {
        private readonly GameController.GameController _gameController;
        private readonly IUIService _uiService;

        private UIlevelSelectWindow _uiSelectWindow;

        public UIlevelSelectWindowController(
            GameController.GameController gameController,
            IUIService uiService)
        {
            _gameController = gameController;
            _uiService = uiService;
            
            _uiSelectWindow = _uiService.Get<UIlevelSelectWindow>();
            _uiSelectWindow.ShowAction += InitButtons;
        }
        public void InitButtons()
        {
            _uiSelectWindow.Buttons[1].OnClick += SelectLevel;
        }

        private void SelectLevel()
        {
            _uiService.Hide<UIlevelSelectWindow>();
            _gameController.StartGame();
            UISubscribeButtons();
        }
        
        public void UISubscribeButtons()
        {
            _uiSelectWindow.Buttons[1].OnClick -= SelectLevel;
        }
    }
}