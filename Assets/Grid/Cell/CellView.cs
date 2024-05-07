using System;
using UnityEngine;
using Zenject;

namespace Grid.Cell
{
    public class CellView : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var coin = other.GetComponent<CoinView>();
                coin.TargetTransformCellPosition(transform.position);
                coin.CellView = this;
        }

        private void OnTriggerExit(Collider other)
        {
            var coin = other.GetComponent<CoinView>();
            coin.TargetTransformCellPosition(Vector3.zero);
            coin.CellView = null;
        }

        public class  Pool : MonoMemoryPool<CellView>
        { }
    }
}