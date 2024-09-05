using System;
using PlayingField;
using Tutorial;
using UI.UILevelSelectWindow;
using UI.UISelectOpponetWindow;
using UI.UIService;
using Unity.VisualScripting;
using UnityEngine;

namespace UI.UITutorialWindow
{
    public class UItutorialWindowController
    {
        private readonly PlayingFieldController _playingFieldController;
        private readonly TutorialConfig _tutorialConfig;
        private readonly IUIService _uiService;

        private UITutorialWindow _uiTutorialWindow;
        private int currentStep;
        
        public UItutorialWindowController(
            PlayingFieldController playingFieldController,
            TutorialConfig tutorialConfig,
            IUIService uiService)
        {
            _playingFieldController = playingFieldController;
            _tutorialConfig = tutorialConfig;
            _uiService = uiService;
            _uiTutorialWindow = _uiService.Get<UITutorialWindow>();

            _uiTutorialWindow.ShowAction += Show;
            _uiTutorialWindow.HideAction += UISubscribeButtons;
        }

        private void Show()
        {
            currentStep = 0;
            Step(0);

            _uiTutorialWindow.Buttons[0].gameObject.SetActive(false);
 
            InitButtons();
        }
        private void InitButtons()
        {
           _uiTutorialWindow.Buttons[0].OnClick += BackStep;
            _uiTutorialWindow.Buttons[1].OnClick += NextStep;
            _uiTutorialWindow.Buttons[2].OnClick += BackToMenu;
        }

        private void Step(int id)
        {
            _uiTutorialWindow.Image.sprite = _tutorialConfig.GetStepModel(id).Sprite;
            _uiTutorialWindow.Text.text = _tutorialConfig.GetStepModel(id).Text;
        }
        private void BackToMenu()
        {
            _uiService.Hide<UITutorialWindow>();
            
            _playingFieldController.ActivateBlackBackground(false);
            _playingFieldController.ChangeBackSpritePosition(BackSpriteType.StartWindow);
            
            _playingFieldController.OnAnimationEnd += ShowStartWindow;
        }

        private void ShowStartWindow()
        {
            _uiService.Show<UIStartWindow.UIStartWindow>();
            _playingFieldController.OnAnimationEnd -= ShowStartWindow;
        }

        private void NextStep()
        {
            if (currentStep<_tutorialConfig.GetCount()-1)
            {
                currentStep++;
                if (currentStep == _tutorialConfig.GetCount()-1)
                {
                    _uiTutorialWindow.Buttons[1].gameObject.SetActive(false);
                }
                else
                {
                    _uiTutorialWindow.Buttons[0].gameObject.SetActive(true);
                }
                Step(currentStep);
            }
        }

        private void BackStep()
        {
            if (currentStep>0)
            {
                currentStep--;
                if (currentStep==0)
                {
                    _uiTutorialWindow.Buttons[0].gameObject.SetActive(false);
                }
                else
                {
                    _uiTutorialWindow.Buttons[1].gameObject.SetActive(true);
                }
                Step(currentStep);
            }
        }

        private void UISubscribeButtons()
        {
            _uiTutorialWindow.Buttons[0].OnClick -= BackStep;
            _uiTutorialWindow.Buttons[1].OnClick -= NextStep;
            _uiTutorialWindow.Buttons[2].OnClick -= BackToMenu;
        }
    }
}