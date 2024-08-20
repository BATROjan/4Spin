using System;
using UnityEngine;
using Zenject;

namespace Grid.Cell
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private SphereCollider _collider;
        [SerializeField] private Material[] materials;
        [SerializeField] private MeshRenderer meshRenderer;
        
        private CoinView _coinView;

        public SphereCollider GetCollier()
        {
            return _collider;
        }

        public MeshRenderer GetMeshRenderer()
        {
            return meshRenderer;
        }
        public Material[] GetMaterial()
        {
            return materials;
        }
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

        public class Pool : MonoMemoryPool<CellView>
        {
        }
    }
}