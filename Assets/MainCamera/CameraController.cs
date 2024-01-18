namespace MainCamera
{
    public class CameraController
    {
        private readonly CameraView _cameraView;

        private CameraController(CameraView cameraView)
        {
            _cameraView = cameraView;
        }
    }
}