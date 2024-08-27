using System;
using Tutorial;
using UI.UILevelSelectWindow;
using UI.UISelectOpponetWindow;
using UI.UIService;
using UnityEngine;

namespace UI.UITutorialWindow
{
    public class UItutorialWindowController
    {
        private readonly TutorialConfig _tutorialConfig;
        private readonly IUIService _uiService;

        private UITutorialWindow _uiTutorialWindow;
        private int currentStep;
        
        public UItutorialWindowController(
            TutorialConfig tutorialConfig,
            IUIService uiService)
        {
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
            Debug.Log(currentStep);
            _uiTutorialWindow.Image.sprite = _tutorialConfig.GetModel(id).Sprite;
            _uiTutorialWindow.Text.text = _tutorialConfig.GetModel(id).Text;
        }
        private void BackToMenu()
        {
            _uiService.Hide<UITutorialWindow>();
            _uiService.Show<UIStartWindow.UIStartWindow>();
        }

        private void NextStep()
        {
            if (currentStep<_tutorialConfig.GetCount()-1)
            {
                currentStep++;
                Step(currentStep);
            }
        }

        private void BackStep()
        {
            if (currentStep>0)
            {
                currentStep--;
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