using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Grid.Cell
{
    public class CellView : MonoBehaviour
    {
        [SerializeField] private SphereCollider _collider;
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
        private void Reinit(Material material)
        {
            meshRenderer.material = material;
        }

        public class Pool : MonoMemoryPool<Material, CellView>
        {
            protected override void Reinitialize(Material material, CellView item)
            {
                item.Reinit(material);
                base.Reinitialize(material, item);
            }

           
        }
    }
}