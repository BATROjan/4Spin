using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioModelConfig", menuName = "Configs/AudioModelConfig")]
public class AudioModelConfig : ScriptableObject
{
    [SerializeField] private AudioModel[] audioModels;
    private Dictionary<AudioType, AudioModel> _dictAudioModels = new Dictionary<AudioType, AudioModel>();
    private List<AudioModel> _audioModelsList = new List<AudioModel>();
    [NonSerialized] private bool _inited;

    public AudioModel GetAudioModelByType(AudioType type)
    {
        if (!_inited)
        {
            Init();
        }

        if (_dictAudioModels.ContainsKey(type))
        {
            return _dictAudioModels[type];
        }
        
        Debug.LogError($"There no such AudioModel with type: {type} ");
        
        return new AudioModel();
    }   
    
    public void SetAudioVolumeByGroup(AudioGroupType type, float volume)
    {
        foreach (var model in audioModels)
        {
            if (model.GroupType == type)
            {
                AudioModel audioModel = new AudioModel();
                audioModel = model;
                audioModel.Volume = volume;
                int id = Array.IndexOf(audioModels, model);
                audioModels[id] = audioModel;
            }
        }
        _dictAudioModels.Clear();
        Init();
    }
    
    private void Init()
    {
        foreach (var model in audioModels)
        {
            
            _dictAudioModels.Add(model.Type, model);
        }

        _inited = true;
    }
}

[Serializable]
public struct AudioModel
{
    public AudioType Type;
    public AudioGroupType GroupType;
    public AudioClip AudioClip;
    public bool Loop;
    public float Volume;
}
public enum AudioType
{
    Background,
    Coin,
    Flip
}
public enum AudioGroupType
{
    Main,
    Effect
}