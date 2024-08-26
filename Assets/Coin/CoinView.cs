using System;
using Coin;
using Grid.Cell;
using UnityEngine;
using Zenject;

public class CoinView : MonoBehaviour
{
    public Action<CellView> OnCellFill;
    public MeshRenderer MeshRenderer => meshRenderer;
    public CellView CellView;
    public Transform CoinPosition;
    public int NumberComand;
    
    private Vector3 _targetTransformPosition;
    private Vector3 _cellTransformPosition;
    
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshCollider meshCollider;
    public void TargetTransformCellPosition(Vector3 position)
    {
        _targetTransformPosition = position;
    }

    public Vector3 GetTargetPosition()
    {
        return _targetTransformPosition;
    }

    public MeshCollider GetMeshCollider()
    {
        return meshCollider;
    }
    public Vector3 GetCellPosition()
    {
        return _cellTransformPosition;
    }
    
    public void CellTransformCellPosition(Vector3 position)
    {
        _cellTransformPosition = position;
    }
    private void ReInit(CoinModel coinModel)
    {
        meshRenderer.material = coinModel.Material;
        NumberComand = coinModel.NumberComand;

        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.localScale = new Vector3(100, 100, 20);
        CellView = null;
    }
    
    public class Pool: MonoMemoryPool<CoinModel, CoinView>
    {
        protected override void Reinitialize(CoinModel material, CoinView item)
        {
            base.Reinitialize(material, item);
            item.ReInit(material);
        }
    }
}
