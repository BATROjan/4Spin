using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayingField
{
    [CreateAssetMenu(fileName = "BackSpriteConfig", menuName = "Configs/BackSpriteConfig")]
    public class BackSpriteConfig : ScriptableObject
    {
        [SerializeField] private BackSpriteModel[] backSpriteModel;
        
        private Dictionary<BackSpriteType, Vector3> _dictionary = new Dictionary<BackSpriteType, Vector3>();
        private bool _isInit;
        
        public Vector3 GetSpritePosition(BackSpriteType type)
        {
            if (!_isInit)
            {
                Init();
            }
            if (_dictionary.TryGetValue(type, out var position))
            {
                return position;
            }
            throw new Exception($"{nameof(type)}: {type} doesn't contains in dictionary");
        }
        
        public void Init()
        {
            foreach (var model in backSpriteModel)
            {
                if (!_dictionary.TryAdd(model.BackSpriteType, model.Position))
                {
                    Debug.LogError($"{model.BackSpriteType} doesn't add to dictionary");
                }
            }
            _isInit = true;
        }

    }
    [Serializable]
    public struct BackSpriteModel
    {
        public BackSpriteType BackSpriteType;
        public Vector3 Position;
    }

    public enum BackSpriteType
    {
        StartWindow, 
        Setting,
        Rules,
        SelectOpponentWindow,
        SelectGridWindow,
        Play
    }
}