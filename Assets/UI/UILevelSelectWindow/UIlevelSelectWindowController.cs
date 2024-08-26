using UI.UIPlayingWindow;
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
            _uiSelectWindow.HideAction += UnSubscribeButtons;
        }
        public void InitButtons()
        {
            _uiSelectWindow.Buttons[1].OnClick += SelectLevel;
        }

        private void SelectLevel()
        {
            _uiService.Hide<UIlevelSelectWindow>();
            _uiService.Show<UIPlayingWindowView>();
            
            _gameController.StartGame(); ;
        }
        
        public void UnSubscribeButtons()
        {
            _uiSelectWindow.Buttons[1].OnClick -= SelectLevel;
        }
    }
}