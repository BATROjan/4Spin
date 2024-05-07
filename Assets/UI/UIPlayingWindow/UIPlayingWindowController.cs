using DG.Tweening;
using UI.UIService;

namespace UI.UIPlayingWindow
{
    public class UIPlayingWindowController
    {
        private readonly IUIService _uiService;
        private UIPlayingWindowView _uiPlayingWindow;
        private float _animationTime = 0.2f;
        
        public UIPlayingWindowController(
            IUIService uiService)
        {
            _uiService = uiService;
            _uiPlayingWindow = _uiService.Get<UIPlayingWindowView>();
            
            _uiPlayingWindow.HideAction += HideLogic;
        }
        public void HideLogic()
        {
            UnSubscribeButtons();
        }
        public void InitButtons()
        {
            _uiPlayingWindow.Buttons[0].OnClick += PowerSelect;
        }

        public void ActivateSliderPanel(bool value)
        {
            InitButtons();
            ShowAnimation(value);
        }

        private void ShowAnimation(bool value)
        {
            if (value)
            {
                _uiPlayingWindow.SliderPanel.DOLocalMove(_uiPlayingWindow.Positions[0], _animationTime);
            }
            else
            {
                _uiPlayingWindow.SliderPanel.DOLocalMove(_uiPlayingWindow.Positions[1], _animationTime);
            }
        }

        private void PowerSelect()
        {
        }

        public void UnSubscribeButtons()
        {
            _uiPlayingWindow.Buttons[0].OnClick -= PowerSelect;
        }
    }
}