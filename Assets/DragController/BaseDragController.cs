using System;
using Grid;
using MainCamera;
using PlayingField;
using UI.UIPlayingWindow;
using UI.UIService;
using UnityEngine;
using Zenject;

namespace DragController
{
    public abstract class BaseDragController : IDragController, ITickable
    {
        public event Action<CoinView, Component> OnPickupItemEvent;

        protected Camera mainCamera;
        protected CoinView coinView;
        protected ColumVew columVew;
        
        protected bool raycastInteractionIsActive;
        protected bool isReadyToSpin;

        private readonly UIPlayingWindowController _uiPlayingWindowController;
        private readonly GridController _gridController;
        private readonly PlayingFieldController _playingFieldController;
        private readonly CameraController _cameraController;
        private readonly TickableManager _tickableManager;
        
        private Collider2D _currentTriggerData;

        protected BaseDragController(
            UIPlayingWindowController uiPlayingWindowController,
            GridController gridController,
            PlayingFieldController playingFieldController,
            CameraController cameraController,
            TickableManager tickableManager)
        {
            _uiPlayingWindowController = uiPlayingWindowController;
            _gridController = gridController;
            _playingFieldController = playingFieldController;
            _cameraController = cameraController;
            _tickableManager = tickableManager;
            _tickableManager.Add(this);
        }

        public virtual void Tick()
        {
            if (!mainCamera)
            {
                mainCamera = _cameraController.GetCameraView().MainCamera;
                return;
            }

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

        public abstract void OnStartRaycastHit(object hits);
        protected abstract void TouchLogic();

        protected void OnTriggerEnter(Collider2D triggerData)
        {
            _currentTriggerData = triggerData;
        }

        protected void OnTriggerExit(Collider2D triggerData)
        {
            _currentTriggerData = null;
        }

        public void OnEndRaycastHit()
        {
            if (coinView)
            {
                if (!coinView.CellView)
                {
                    coinView.transform.localPosition = coinView.GetCellPosition();
                }
                else
                {
                    if (!isReadyToSpin)
                    {
                        coinView.transform.SetParent(coinView.CellView.transform);
                        coinView.transform.localPosition = Vector3.zero;
                        _playingFieldController.SetActiveArrows(true);
                        _playingFieldController.SetActiveCoins(true);
                        _gridController.SetActiveColums(true);
                        isReadyToSpin = true;
                    }
                }

                coinView.gameObject.layer = 0;
                coinView = null;
            }
        }

        public void StartRaycastInteraction()
        {
            if (raycastInteractionIsActive)
            {
                return;
            }
            raycastInteractionIsActive = true;
        }

        public void StopRaycastInteraction()
        {
            if (!raycastInteractionIsActive)
            {
                return;
            }
            raycastInteractionIsActive = false;

            OnEndRaycastHit();
        }
    }
}