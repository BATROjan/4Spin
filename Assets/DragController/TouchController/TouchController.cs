using Environment;
using Grid;
using MainCamera;
using PlayingField;
using UI.UIPlayingWindow;
using UnityEngine;
using Zenject;

namespace DragController.TouchController
{
    public class TouchController: BaseDragController
    {
        public TouchController(
            AudioController.AudioController audioController,
            EnvironmentController environmentController,
            UIPlayingWindowController uiPlayingWindowController, 
            GridController gridController, 
            PlayingFieldController playingFieldController, 
            CameraController cameraController, 
            TickableManager tickableManager) : 
            base(
                audioController,
                environmentController,
                uiPlayingWindowController, 
                gridController, 
                playingFieldController, 
                cameraController, 
                tickableManager)
        {
        }     
        public override void Tick()
        {
            base.Tick();
            
            if (raycastInteractionIsActive)
            {
                if (Input.touchCount > 0)
                {
                    TouchLogic();
                }
                else
                {
                    OnEndRaycastHit();
                }
            }
        }

        protected override void TouchLogic()
        {
            var ray = mainCamera.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out _hit, _distance, 3))
            {
                if (_hit.transform)
                {
                    OnStartRaycastHit(_hit);
                }
            }
        }
    }
}