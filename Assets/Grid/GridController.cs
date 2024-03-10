using System.Collections.Generic;
using Grid.Cell;
using PlayingField;
using UnityEngine;

namespace Grid
{
    public class  GridController
    {
        private readonly PlayingFieldController _playingFieldController;
        private readonly ColumVew.Pool _columViewPool;
        private readonly CellView.Pool _cellViewPool;
        private readonly GridConfig _gridConfig;

        private List<CellView> _cellViewsList = new List<CellView>();
        private List<ColumVew> _columViewsList = new List<ColumVew>();

        private Vector3 _firstPosition = Vector3.zero;

        private GridModel _currentGridModel;
        private PlayingFieldView _playingFieldView;

        public GridController(
            PlayingFieldController playingFieldController,
            ColumVew.Pool columViewPool,
            CellView.Pool cellViewPool,
            GridConfig gridConfig
        )
        {
            _playingFieldController = playingFieldController;
            _columViewPool = columViewPool;
            _cellViewPool = cellViewPool;
            _gridConfig = gridConfig;
        }

        public void SpawnGrid()
        { 
            _currentGridModel = _gridConfig.GetGrid(DiffcultLevel.Normal);
            
          var playingFieldView = _playingFieldController.SpawnPlayingVew(_currentGridModel.diffcultLevel);
          _playingFieldView = playingFieldView;
          
          SpawnColums();
            
            for (int i = 0; i < _currentGridModel.columnCount; i++)
            {
                List<CellView> someCurrentCells = new List<CellView>();

                for (int j = 0; j < _currentGridModel.lineCount; j++)
                {
                    
                    var cellPosition = _firstPosition + 3.8f * new Vector3(i, j, 0);
                    var newCell = _cellViewPool.Spawn();
                    
                    newCell.transform.position = cellPosition;
                    _cellViewsList.Add(newCell);
                    
                    someCurrentCells.Add(newCell);
                }

                foreach (var cell in someCurrentCells)
                {
                    cell.transform.SetParent(_columViewsList[i].transform, false);
                }
            }

            foreach (var colum in _columViewsList)
            {
                colum.transform.SetParent(_playingFieldView.Colums);
            }
        }

        private void SpawnColums()
        {
            for (int i = 0; i < _currentGridModel.columnCount; i++)
            {
                var columPosition = _firstPosition + 3.8f * new Vector3(i, 0, 0);
                var colum = _columViewPool.Spawn();
                _columViewsList.Add(colum);
            }
            
        }
    }
}