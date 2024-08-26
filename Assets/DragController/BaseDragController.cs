using System;
using Environment;
using Grid;
using MainCamera;
using PlayingField;
using Slider;
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
        
        protected RaycastHit _hit;
        protected const int _distance = 1000;
            
        protected bool raycastInteractionIsActive;
        protected bool isReadyToSpin;

        private readonly SliderController _sliderController;
        private readonly EnvironmentController _environmentController;
        private readonly UIPlayingWindowController _uiPlayingWindowController;
        private readonly GridController _gridController;
        private readonly PlayingFieldController _playingFieldController;
        private readonly CameraController _cameraController;
        private readonly TickableManager _tickableManager;
        
        private Collider2D _currentTriggerData;

        protected BaseDragController(
            EnvironmentController environmentController,
            UIPlayingWindowController uiPlayingWindowController,
            GridController gridController,
            PlayingFieldController playingFieldController,
            CameraController cameraController,
            TickableManager tickableManager)
        {
            _environmentController = environmentController;
            _uiPlayingWindowController = uiPlayingWindowController;
            _gridController = gridController;
            _playingFieldController = playingFieldController;
            _cameraController = cameraController;

            _gridController.OnSpinningIsDone += ClearAll;
            _environmentController.OnNexStep += SetCoinToCell;
            _tickableManager = tickableManager;
            _tickableManager.Add(this);
        }

        public virtual void Tick()
        {
            if (!mainCamera)
            {
                mainCamera = _cameraController.GetCameraView().MainCamera;
            }
        }
        protected abstract void TouchLogic();

        protected void OnTriggerEnter(Collider2D triggerData)
        {
            _currentTriggerData = triggerData;
        }

        protected void OnTriggerExit(Collider2D triggerData)
        {
            _currentTriggerData = null;
        }

        public void OnStartRaycastHit(object hit)
        {
            if (!isReadyToSpin)
            {
                if (!coinView)
                {
                    coinView = ((RaycastHit)hit).transform.GetComponent<CoinView>();
                   
                        if (coinView )
                        {
                            if (coinView.gameObject.layer == 0)
                            {
                                coinView.gameObject.layer = 3;
                            }
                        }
                    
                }
                else
                {
                    coinView.transform.position = ((RaycastHit)hit).point;
                }
            }
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
                        SetCoinToCell(coinView);
                    }
                }

                coinView.gameObject.layer = 0;
                coinView = null;
            }
        }

        public void SetCoinToCell(CoinView coin)
        {
            coin.CellView.GetCollier().enabled = false;
            coin.transform.SetParent(coin.CellView.transform);
            coin.OnCellFill?.Invoke(coin.CellView);
            coin.transform.localPosition = Vector3.zero;
            
            _gridController.RemoveCelFromList(coinView.CellView);
            _gridController.SetActiveCoinCollider(coin, false);
            _gridController.ResetCoinView();
            
            _playingFieldController.SetActiveCoin(false);

            isReadyToSpin = true;
        }

        public void ClearAll()
        {
            coinView = null;
            columVew = null;
            isReadyToSpin = false;
            
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