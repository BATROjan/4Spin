using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
                 case DiffcultLevel.Easy:
                    currenrModel = playingFieldModels[0];
                    break;
                case DiffcultLevel.Normal :
                    currenrModel= playingFieldModels[1];
                    break;
                case DiffcultLevel.Hard :
                    currenrModel= playingFieldModels[2];
                    break;
            }
            return currenrModel;
        }
    }

    [Serializable]
    public struct PlayingFieldModel
    {
        public Vector3 FieldPosition;
        public Vector3 CurrentCoinPosition;
        public Transform CylinderTransform;
        public Vector3 LeftSpherePosition;
        public Vector3 RightSpherePosition;
    }
}