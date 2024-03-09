using UI.UILevelSelectWindow;
using UI.UIService;
using UnityEngine.EventSystems;

namespace UI.UIStartWindow
{
    public class UIStartWindowController
    {
        private readonly IUIService _uiService;

        private UIStartWindow _uiStartWindow;

        public UIStartWindowController(
            IUIService uiService)
        {
            _uiService = uiService;
            _uiStartWindow = _uiService.Get<UIStartWindow>();
            
            _uiStartWindow.ShowAction += InitButtons;

            _uiService.Show<UIStartWindow>();
        }

        public void InitButtons()
        {
            _uiStartWindow.Buttons[0].OnClick += SelectLevel;
        }

        private void SelectLevel()
        {
            _uiService.Hide<UIStartWindow>();
            _uiService.Show<UIlevelSelectWindow>();
            
            UISubscribeButtons();
        }
        
        public void UISubscribeButtons()
        {
            _uiStartWindow.Buttons[0].OnClick -= SelectLevel;
        }
    }
}