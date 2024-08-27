using UnityEngine;

namespace GameController
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]

    public class GameConfig: ScriptableObject
    {
        public DiffcultLevel DiffcultLevel
        {
            get => diffcultLevel;
            set => diffcultLevel = value;
        }

        public bool IsPvE
        {
            get => isPvE;
            set => isPvE = value;
        }

        [SerializeField] private bool isPvE;
        
        private DiffcultLevel diffcultLevel;
    }
}