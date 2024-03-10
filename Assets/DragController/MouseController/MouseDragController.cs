using MainCamera;
using UnityEngine;
using Zenject;

namespace DragController.MouseController
{
    public class MouseDragController : BaseDragController
    {
        public float raycastDistance = 1000f;  
        protected Vector3 worldTouchPosition;
        
        private RaycastHit2D _hit;
        
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
                worldTouchPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                if (Input.GetMouseButton(0))
                {
                    TouchLogic();
                }
            }
        }

        public override void OnStartRaycastHit(RaycastHit2D hits)
        {
            throw new System.NotImplementedException();
        }

        protected override void TouchLogic()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                Debug.Log("Попал в объект: " + hit.transform.name);
                
                // Добавь здесь свой код для обработки столкновения луча с объектом
            }
            
            Debug.DrawRay(ray.origin,ray.direction, Color.green);
        }
    }
}