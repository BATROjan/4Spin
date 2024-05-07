using UnityEngine;
using UnityEngine.Serialization;

namespace PlayingField
{
    public class CoinSpawnView : MonoBehaviour
    {
        [SerializeField] private Vector3 hidePosition;
        [SerializeField] private Vector3 ShowPosition;

        public Vector3 GetShowPosition()
        {
            return ShowPosition;
        } 
        
        public Vector3 GetHidePosition()
        {
            return hidePosition;
        }
    }
}