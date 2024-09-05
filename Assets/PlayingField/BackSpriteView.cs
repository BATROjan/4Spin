using UnityEngine;
using Zenject;
using Vector3 = System.Numerics.Vector3;

namespace PlayingField
{
    public class BackSpriteView : MonoBehaviour
    {
        public SpriteRenderer BlackSprite => blackSprite;
        
        public Vector3 ViewPosition
        {
            get => viewPosiotion;
            set => viewPosiotion = value;
        }
        public SpriteRenderer ViewSprite
        {
            get => viewSprite;
            set => viewSprite = value;
        }
        
        [SerializeField] private SpriteRenderer viewSprite;
        [SerializeField] private SpriteRenderer blackSprite;
        
        private Vector3 viewPosiotion;
        
        public class  Pool : MonoMemoryPool<BackSpriteView>
        {
        }
    }
}