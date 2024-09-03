using Environment;
using Grid;
using MainCamera;
using PlayingField;
using Slider;
using UI.UIPlayingWindow;
using UnityEngine;
using Zenject;

namespace DragController.MouseController
{
    public class MouseDragController : BaseDragController
    {
        private readonly UIPlayingWindowController _uiPlayingWindowController;
        private readonly GridController _gridController;
        private readonly PlayingFieldController _playingFieldController;
        
        public MouseDragController( 
            AudioController.AudioController audioController,
            EnvironmentController environmentController,
            UIPlayingWindowController uiPlayingWindowController,
            GridController gridController,
            PlayingFieldController playingFieldController,
            CameraController cameraController, 
            TickableManager tickableManager) 
            : base(
                audioController,
                environmentController,
                uiPlayingWindowController,
                gridController,
                playingFieldController, 
                cameraController,
                tickableManager)
        {
            _uiPlayingWindowController = uiPlayingWindowController;
            _gridController = gridController;
            _playingFieldController = playingFieldController;
        }

        public override void Tick()
        {
            base.Tick();
            
            if (raycastInteractionIsActive)
            {
                if (Input.GetMouseButton(0))
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
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

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