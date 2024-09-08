using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{   
    [CreateAssetMenu(fileName = "TutorialConfig", menuName = "Configs/TutorialConfig")]
    
    public class TutorialConfig: ScriptableObject
    {
        [SerializeField] private TutorStepModel[] tutorStepModel;
        public TutorStepModel GetStepModel(int id)
        {
            return tutorStepModel[id];
        }
        
        public int GetCount()
        {
            return tutorStepModel.Length;
        }
    }
    
    [Serializable]
    public struct TutorStepModel
    {
        public Sprite Sprite;
        public String Text;
    }  
}