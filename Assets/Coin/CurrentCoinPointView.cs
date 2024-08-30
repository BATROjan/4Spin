using UnityEngine;

namespace Coin
{
    public class CurrentCoinPointView : MonoBehaviour
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

        public void ChangeShowPosition(Vector3 position)
        {
            ShowPosition = position;
        }
    }
}