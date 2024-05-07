using Grid;
using MainCamera;
using PlayingField;
using UI.UIPlayingWindow;
using UnityEngine;
using Zenject;

namespace DragController.MouseController
{
    public class MouseDragController : BaseDragController
    {
        private readonly UIPlayingWindowController _uiPlayingWindowController;
        private const int Distance = 1000;
        
        private RaycastHit _hit;
        
        public MouseDragController(
            UIPlayingWindowController uiPlayingWindowController,
            GridController gridController,
            PlayingFieldController playingFieldController,
            CameraController cameraController, 
            TickableManager tickableManager) 
            : base(
                uiPlayingWindowController,
                gridController,
                playingFieldController, 
                cameraController,
                tickableManager)
        {
            _uiPlayingWindowController = uiPlayingWindowController;
        }

        public override void OnStartRaycastHit(object hit)
        {
            if (!isReadyToSpin)
            {
                if (!coinView)
                {
                    coinView = ((RaycastHit)hit).transform.GetComponent<CoinView>();
                    if (coinView)
                    {
                        coinView.gameObject.layer = 3;
                    }
                }
                else
                {
                    coinView.transform.position = ((RaycastHit)hit).point;
                }
            }
            else
            {
                if (!columVew)
                {
                    columVew = ((RaycastHit)hit).transform.GetComponent<ColumVew>();
                    _uiPlayingWindowController.ActivateSliderPanel(true);
                }
            }
        }

        protected override void TouchLogic()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hit, Distance, 3))
            {
                if (_hit.transform)
                {
                    Debug.Log("Попал в объект: " + _hit.transform.name);
                    
                    OnStartRaycastHit(_hit);
                }
            }
        }
    }
}