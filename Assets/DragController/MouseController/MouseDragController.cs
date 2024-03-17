using MainCamera;
using UnityEngine;
using Zenject;

namespace DragController.MouseController
{
    public class MouseDragController : BaseDragController
    {
        private const int Distance = 1000;
        
        private RaycastHit _hit;
        
        public MouseDragController(
            CameraController cameraController, 
            TickableManager tickableManager) 
            : base(cameraController, tickableManager)
        {
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

        public override void OnStartRaycastHit(object hits)
        {
            if (!coinView)
            {
                coinView = _hit.transform.GetComponent<CoinView>();
            }
            else
            {
                float deltaX = Input.GetAxis("Mouse X");
                float deltaY = Input.GetAxis("Mouse Y");
                
                Vector3 newPosition = _hit.transform.position + new Vector3(deltaX, deltaY, 0);
                
                coinView.transform.position = newPosition;
            }
        }

        protected override void TouchLogic()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out _hit, Distance))
            {
                Debug.Log("Попал в объект: " + _hit.transform.name);
                if (_hit.collider.GetComponentInChildren<CoinView>())
                {
                    if (_hit.transform != null)
                    {
                        OnStartRaycastHit(_hit);
                    }
                }
            }
        }
    }
}