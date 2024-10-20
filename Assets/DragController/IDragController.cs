using System;
using UnityEngine;

namespace DragController
{
    public interface IDragController
    {
        event Action<CoinView, Component> OnPickupItemEvent;
        void OnStartRaycastHit(object hit);
        void OnEndRaycastHit();
        void ClearAll();
        void StartRaycastInteraction();
        void StopRaycastInteraction();
    }
}