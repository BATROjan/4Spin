using System;
using System.Collections.Generic;
using PlayingField;
using UI.UIService;
using UnityEngine;
using XMLSystem;

namespace UI.UISettingsWindow
{
    public class UISettingsWindowController
    {
        private readonly IXMLSystem _xmlSystem;
        private readonly PlayingFieldController _playingFieldController;
        private readonly AudioController.AudioController _audioController;
        private readonly AudioModelConfig _audioModelConfig;
        private readonly IUIService _uiService;
        
        private UISettingsWindowView _settingsWindowView;

        public UISettingsWindowController(
            IXMLSystem xmlSystem,
            PlayingFieldController playingFieldController,
            AudioController.AudioController audioController,
            AudioModelConfig audioModelConfig,
            IUIService uiService)
        {
            _xmlSystem = xmlSystem;
            _playingFieldController = playingFieldController;
            _audioController = audioController;
            _audioModelConfig = audioModelConfig;
            _uiService = uiService;
            _settingsWindowView = _uiService.Get<UISettingsWindowView>();
            _settingsWindowView.ShowAction += Show;
            _settingsWindowView.Sliders[0].onValueChanged.AddListener(delegate {ChangeMainValue();}); ;
        }

        private void Show()
        {
            _settingsWindowView.Sliders[0].value = _audioModelConfig.GetAudioModelByType(AudioType.Background).Volume;
            _settingsWindowView.Sliders[1].value = _audioModelConfig.GetAudioModelByType(AudioType.Coin).Volume;
            
            _settingsWindowView.SaveButton.OnClick += Hide;
        }

        private void Hide()
        {
            SaveSettings();
            _uiService.Hide<UISettingsWindowView>();
            
            _playingFieldController.ChangeBackSpritePosition(BackSpriteType.StartWindow);
            _playingFieldController.OnAnimationEnd += ShowStartWindow;
            
            _settingsWindowView.SaveButton.OnClick -= Hide;
        }

        private void ShowStartWindow()
        {
            _uiService.Show<UIStartWindow.UIStartWindow>();
            _playingFieldController.OnAnimationEnd -= ShowStartWindow;
        }

        private void SaveSettings()
        {
            var volume = (float)Math.Round(_settingsWindowView.Sliders[0].value, 2);
            _audioModelConfig.SetAudioVolumeByGroup(AudioGroupType.Main, volume);
            _xmlSystem.SaveSoundValueToXML(AudioGroupType.Main, volume.ToString());
            
            volume = (float)Math.Round(_settingsWindowView.Sliders[1].value, 2);
            _audioModelConfig.SetAudioVolumeByGroup(AudioGroupType.Effect, volume);
            _xmlSystem.SaveSoundValueToXML(AudioGroupType.Effect, volume.ToString());
        }

        public void ChangeMainValue()
        {
            var volume = (float)Math.Round(_settingsWindowView.Sliders[0].value, 2);
            _audioController.ChangeVolume(volume);
        }
    }
}