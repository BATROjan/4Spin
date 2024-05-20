using Coin;
using Grid.Cell;
using UnityEngine;
using Zenject;

public class CoinView : MonoBehaviour
{
    public CellView CellView;
    public Transform CoinPosition;
    public int NumberComand;
    
    private Vector3 _targetTransformPosition;
    private Vector3 _cellTransformPosition;
    
    [SerializeField] private MeshRenderer meshRenderer;

    public void TargetTransformCellPosition(Vector3 position)
    {
        _targetTransformPosition = position;
    }

    public Vector3 GetTargetPosition()
    {
        return _targetTransformPosition;
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
