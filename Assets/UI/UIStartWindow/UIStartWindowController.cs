using UI.UILevelSelectWindow;
using UI.UISelectOpponetWindow;
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
            _uiStartWindow.HideAction += UnSubscribeButtons;

            _uiService.Show<UIStartWindow>();
        }

        public void InitButtons()
        {
            _uiStartWindow.Buttons[0].OnClick += SelectLevel;
            _uiStartWindow.Buttons[1].OnClick += StartTutorial;
        }

        private void StartTutorial()
        {
            _uiService.Hide<UIStartWindow>();
            _uiService.Show<UITutorialWindow.UITutorialWindow>();
        }

        private void SelectLevel()
        {
            _uiService.Hide<UIStartWindow>();
            _uiService.Show<UISelectOpponentWindow>();
        }
        
        public void UnSubscribeButtons()
        {
            _uiStartWindow.Buttons[0].OnClick -= SelectLevel;
            _uiStartWindow.Buttons[1].OnClick -= StartTutorial;
        }
    }
}