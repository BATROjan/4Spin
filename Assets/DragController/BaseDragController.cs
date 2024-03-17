using System;
using MainCamera;
using UnityEngine;
using Zenject;

namespace DragController
{
    public abstract class BaseDragController : IDragController, ITickable
    {
        public event Action<CoinView, Component> OnPickupItemEvent;

        protected Camera mainCamera;
        protected CoinView coinView;

        protected bool raycastInteractionIsActive;
        protected bool isDrag;
        
        private readonly CameraController _cameraController;
        private readonly TickableManager _tickableManager;
        
        private Collider2D _currentTriggerData;

        protected BaseDragController(
            CameraController cameraController,
            TickableManager tickableManager)
        {
            _cameraController = cameraController;
            _tickableManager = tickableManager;
            _tickableManager.Add(this);
        }

        public virtual void Tick()
        {
            if (mainCamera == null)
            {
                mainCamera = _cameraController.GetCameraView().MainCamera;
                return;
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
            coinView = null;
            isDrag = false;
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