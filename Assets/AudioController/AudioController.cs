using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using XMLSystem;

namespace AudioController
{
    public class AudioController
    {
        private readonly IXMLSystem _xmlSystem;
        private readonly AudioModelConfig _audioModelConfig;
        private readonly AudioView.Pool _audioPool;
        
        private List<AudioView> _listAudioView = new List<AudioView>();
        private Dictionary<AudioType, AudioView> _dictionary = new Dictionary<AudioType, AudioView>();
        
        public AudioController(
            IXMLSystem xmlSystem,
            AudioModelConfig audioModelConfig,
            AudioView.Pool audioPool)
        {
            _xmlSystem = xmlSystem;
            _audioModelConfig = audioModelConfig;
            _audioPool = audioPool;

            audioModelConfig.SetAudioVolumeByGroup(AudioGroupType.Effect, 
                float.Parse(_xmlSystem.LoadFromXML(AudioGroupType.Effect.ToString(), "value")));
            audioModelConfig.SetAudioVolumeByGroup(AudioGroupType.Main,
                float.Parse(_xmlSystem.LoadFromXML(AudioGroupType.Main.ToString(), "value")));
        }
        public void Play(AudioType nameSound,float pitchValue, bool isLoop = false, bool isFlip = false)
        {
            var audio = _audioPool.Spawn(new AudioProtocol(_audioModelConfig.GetAudioModelByType(nameSound)));
            audio.AudioSource.pitch = pitchValue;
            audio.AudioSource.Play();
        
            _listAudioView.Add(audio);
            _dictionary.Add(nameSound, audio);
            if (!isLoop)
            {
                DOVirtual.DelayedCall(audio.AudioSource.clip.length, () =>
                {
                    DespawnAudio(nameSound, audio);
                });
            }

            if (isFlip)
            {
                DOVirtual.DelayedCall(1.5f, () =>
                {
                    audio.AudioSource.DOPitch(0, 1.5f).OnComplete(() =>
                    {
                        DespawnAudio(nameSound, audio);
                    });
                });
            }
        }

        public void DespawnAudio(AudioType nameSound, AudioView audio)
        {
            audio.AudioSource.clip = null;
                    
            _audioPool.Despawn(audio);
            _listAudioView.Remove(audio);
            _dictionary.Remove(nameSound);
        }

        public void ChangeVolume(float volume)
        {
            foreach (var audio in _listAudioView)
            {
                audio.AudioSource.volume = volume;
            }
        }

        public void ActivateMuteEffect(bool value)
        {
            foreach (var audio in _listAudioView)
            {
                if (audio.GroupType == AudioGroupType.Effect)
                {
                    audio.AudioSource.mute = value;
                }
            }
        }
    }
}