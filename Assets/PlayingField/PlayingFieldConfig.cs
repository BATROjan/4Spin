using System;
using System.Collections.Generic;
using UnityEngine;

namespace PlayingField
{
    [CreateAssetMenu(fileName = "PlayingFieldConfig", menuName = "Configs/PlayingFieldConfig")]

    public class PlayingFieldConfig : ScriptableObject
    {
        [SerializeField] private PlayingFieldModel[] playingFieldModels;

        public PlayingFieldModel GetField(DiffcultLevel diffcultLevel)
        {
            PlayingFieldModel currenrModel = default;

            switch (diffcultLevel)
            {
                case DiffcultLevel.Normal:
                    currenrModel = playingFieldModels[0];
                    break;
            }
            return currenrModel;
        }
    }

    [Serializable]
    public struct PlayingFieldModel
    {
        public GameObject Cylindr;
        public Transform CylindrTransfom;
        public GameObject[] Colums;
        
    }
}