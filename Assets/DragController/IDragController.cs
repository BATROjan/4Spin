using System;
using UnityEngine;

namespace DragController
{
    public interface IDragController
    {
        event Action<CoinView, Component> OnPickupItemEvent;
        void OnStartRaycastHit(object hits);
        void OnEndRaycastHit();
        void StartRaycastInteraction();
        void StopRaycastInteraction();
    }
}