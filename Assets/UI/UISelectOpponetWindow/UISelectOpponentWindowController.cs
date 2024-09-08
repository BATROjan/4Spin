using DG.Tweening;
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
        private Sequence animateSequence;
        private OponentView currentOponentView;
        private OponentView notCurrentOponentView;
        
        private bool isProveButtonsActicate;
        private float _animationTime = 0.5f;
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
            _uiSelectOpponentWindow.Buttons[0].OnClick += SelectComputer;
            _uiSelectOpponentWindow.Buttons[1].OnClick += SelectPlayer;
            _uiSelectOpponentWindow.Buttons[2].OnClick += BackToSelect;
            _uiSelectOpponentWindow.Buttons[3].OnClick += SelectOpponent;

        }

        private void SelectPlayer()
        {
            IdentifyOponentViews(1);

            _uiSelectOpponentWindow.Buttons[0].gameObject.SetActive(false); 
            _uiSelectOpponentWindow.Buttons[1].gameObject.SetActive(false); 
            
            isProveButtonsActicate = false;
            _gameController.SetPvE(false);
            
            AnimateIcons();
        }

        private void SelectComputer()
        {
            IdentifyOponentViews(0);
            
            _uiSelectOpponentWindow.Buttons[0].gameObject.SetActive(false); 
            _uiSelectOpponentWindow.Buttons[1].gameObject.SetActive(false); 
            
            isProveButtonsActicate = false;
            
            _gameController.SetPvE(true);
            AnimateIcons();
        }

        private void IdentifyOponentViews(int id)
        {
            currentOponentView = _uiSelectOpponentWindow.Oponents[id];
            foreach (var view in _uiSelectOpponentWindow.Oponents)
            {
                if (currentOponentView != view)
                {
                    notCurrentOponentView = view;
                }
            }
        }

        private void BackToSelect()
        {
            _uiSelectOpponentWindow.Buttons[2].gameObject.SetActive(false); 
            _uiSelectOpponentWindow.Buttons[3].gameObject.SetActive(false); 
            AnimateIcons();
        }

        private void AnimateIcons()
        {
            if (!isProveButtonsActicate)
            {
                currentOponentView.transform.DOScale(1.3f, _animationTime).OnComplete(() =>
                {
                    ShowProveButtons(true);
                    isProveButtonsActicate = true;
                });
                notCurrentOponentView.transform.DOScale(0.4f, _animationTime);
                foreach (var image in notCurrentOponentView.Images)
                {
                    image.DOFade(0.1f, _animationTime);
                }
            }
            else
            {
                foreach (var image in notCurrentOponentView.Images)
                {
                    image.DOFade(1, _animationTime);
                }
                notCurrentOponentView.transform.DOScale(1, _animationTime);
                currentOponentView.transform.DOScale(1, _animationTime).OnComplete(() =>
                {
                    ShowProveButtons(false);
                    isProveButtonsActicate = false;

                });
                
            }
        }

        private void ShowProveButtons(bool value)
        {
            if (value)
            {
                _uiSelectOpponentWindow.Buttons[2].gameObject.SetActive(true); 
                _uiSelectOpponentWindow.Buttons[3].gameObject.SetActive(true); 
            }
            else
            {
                _uiSelectOpponentWindow.Buttons[0].gameObject.SetActive(true); 
                _uiSelectOpponentWindow.Buttons[1].gameObject.SetActive(true); 
            }
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
            _uiSelectOpponentWindow.Buttons[0].OnClick -= SelectComputer;
            _uiSelectOpponentWindow.Buttons[1].OnClick -= SelectPlayer;
            _uiSelectOpponentWindow.Buttons[2].OnClick -= BackToSelect;
            _uiSelectOpponentWindow.Buttons[3].OnClick -= SelectOpponent;
        }
    }
}