using System;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{   
    [CreateAssetMenu(fileName = "TutorialConfig", menuName = "Configs/TutorialConfig")]
    
    public class TutorialConfig: ScriptableObject
    {
        [SerializeField] private TutorStepModel[] tutorStepModel;
        [SerializeField] private TutorViewModel[] tutorViewModels;
        public TutorStepModel GetStepModel(int id)
        {
            return tutorStepModel[id];
        }

        public TutorialFieldView GetView(DiffcultLevel diffcultLevel)
        {
            TutorialFieldView view = new TutorialFieldView();
            
            foreach (var tutorView in tutorViewModels)
            {
                if (tutorView.DiffcultLevel == diffcultLevel)
                {
                    view = tutorView.View;
                }
            }
            
            return view;
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
    [Serializable]
    public struct TutorViewModel
    {
        public DiffcultLevel DiffcultLevel;
        public TutorialFieldView View;
    }
}