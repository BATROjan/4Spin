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
            if (coin)
            {
                coin.CellView = this;
                coin.TargetTransformCellPosition(transform.position);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var coin = other.GetComponent<CoinView>();
            if (coin)
            {
                coin.TargetTransformCellPosition(Vector3.zero);
                coin.CellView = null;
            }
        }

        public class  Pool : MonoMemoryPool<CellView>
        { }
    }
}