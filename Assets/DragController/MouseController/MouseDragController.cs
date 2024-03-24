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

        public override void OnStartRaycastHit(object hit)
        {
            if (!coinView)
            {
                coinView = ((RaycastHit)hit).transform.GetComponent<CoinView>();
                if (coinView)
                { 
                coinView.gameObject.layer = 3;
                Debug.Log("AAAAAAAA");
                
                }
            }
            else
            {
                coinView.transform.position = ((RaycastHit)hit).point;
                Debug.Log("BBBBBB");

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