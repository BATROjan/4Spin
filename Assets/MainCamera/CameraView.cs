using UnityEngine;

namespace MainCamera
{
    public class CameraView: MonoBehaviour
    {
        public Camera MainCamera => mainCamera;
        
        [SerializeField] private Camera mainCamera;
    }
}