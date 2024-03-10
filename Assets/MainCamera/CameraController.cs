namespace MainCamera
{
    public class CameraController
    {
        private CameraView _cameraView;

        private CameraController(CameraView cameraView)
        {
            _cameraView = cameraView;
        }
        
        public CameraView GetCameraView()
        {
            return _cameraView;
        }
    }
}