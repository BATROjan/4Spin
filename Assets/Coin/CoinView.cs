using System.Collections;
using System.Collections.Generic;
using Coin;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Zenject;

public class CoinView : MonoBehaviour
{
    public Vector3 CellTransformPosition;
    
    [SerializeField] private MeshRenderer meshRenderer;
    private void ReInit(CoinModel coinModel)
    {
        meshRenderer.material = coinModel.Material;
    }
    public class Pool: MonoMemoryPool<CoinModel, CoinView>
    {
        protected override void Reinitialize(CoinModel coinModel, CoinView item)
        {
            base.Reinitialize(coinModel, item);
            item.ReInit(coinModel);
        }
    }
}
