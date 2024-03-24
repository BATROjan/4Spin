using System;
using UnityEngine;
using Zenject;

namespace Grid.Cell
{
    public class CellView : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            other.GetComponent<CoinView>().CellTransformPosition = transform.position;
        }

        private void OnTriggerExit(Collider other)
        {
            other.GetComponent<CoinView>().CellTransformPosition = Vector3.zero;
        }

        public class  Pool : MonoMemoryPool<CellView>
        {
            
        }
    }
}