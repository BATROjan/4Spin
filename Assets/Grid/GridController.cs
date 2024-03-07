using Grid.Cell;
using UnityEngine;

namespace Grid
{
    public class GridController
    {
        private readonly CellView.Pool _cellViewPool;
        private readonly GridConfig _gridConfig;
        
        private Vector3 firstCellPosition = Vector3.zero;

        private GridModel _currentGridModel;

        public GridController(
            CellView.Pool cellViewPool,
            GridConfig gridConfig
        )
        {
            _cellViewPool = cellViewPool;
            _gridConfig = gridConfig;
        }

        public void SpawnGrid()
        {
            _currentGridModel = _gridConfig.GetGrid(DiffcultLevel.Normal);
            for (int i = 0; i < _currentGridModel.columnCount; i++)
            {
                for (int j = 0; j < _currentGridModel.lineCount; j++)
                {
                    var cellPosition = firstCellPosition + 3.8f * new Vector3(i, j, 0);
                    var newCell = _cellViewPool.Spawn();
                    newCell.transform.position = cellPosition;
                }
            }
           
        }
        
        private void Init()
        {
            /*for (var i = 0; i < _model.ColumnsCellsNumber; i++)
            {
                for (var j = 0; j < _model.RowsCellsNumber; j++)
                {
                    var cellPosition = _firstCellPos + _offset * new Vector3(i, j, 0);
                    var newCell = _gameCellPool.Spawn(cellPosition, _uiRoot.GridCells);

                    _cells[i, j] = newCell;
                    _cells[i, j].X = i;
                    _cells[i, j].Y = j;
                }
            }*/
        }

    }
}