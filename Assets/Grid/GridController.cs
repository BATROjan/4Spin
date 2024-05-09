using System;
using System.Collections.Generic;
using DG.Tweening;
using Grid.Cell;
using PlayingField;
using Slider;
using UnityEngine;

namespace Grid
{
    public class  GridController
    {
        public ColumVew CurrentColum;
        public Action OnSpinningIsDone;
        
        private readonly SliderController _sliderController;
        private readonly CoinController _coinController;
        private readonly PlayingFieldController _playingFieldController;
        private readonly ColumVew.Pool _columViewPool;
        private readonly CellView.Pool _cellViewPool;
        private readonly GridConfig _gridConfig;

        private List<CellView> _cellViewsList = new List<CellView>();
        private List<ColumVew> _columViewsList = new List<ColumVew>();

        private Dictionary<int, List<CellView>> _dictionaryOfColums = new Dictionary<int, List<CellView>>();
        
        private Vector3 _firstColumPosition = new Vector3(-5.6f,0,0);
        private Vector3 _firstCellPosition = new Vector3(0,-9.58f,0);

        private GridModel _currentGridModel;
        private PlayingFieldView _playingFieldView;
        private CellView[,] _cellViews;
        
        public GridController(SliderController sliderController,
            CoinController coinController,
            PlayingFieldController playingFieldController,
            ColumVew.Pool columViewPool,
            CellView.Pool cellViewPool,
            GridConfig gridConfig
        )
        {
            _sliderController = sliderController;
            _coinController = coinController;
            _playingFieldController = playingFieldController;
            _columViewPool = columViewPool;
            _cellViewPool = cellViewPool;
            _gridConfig = gridConfig;

            _sliderController.GetSliderView().UIButtons[0].OnClick += SpinColum;
        }

        public void SpawnGrid()
        { 
            _currentGridModel = _gridConfig.GetGrid(DiffcultLevel.Normal);
            
            var playingFieldView = _playingFieldController.SpawnPlayingVew(_currentGridModel.diffcultLevel);
            _playingFieldView = playingFieldView;
            
            _cellViews = new CellView[_currentGridModel.lineCount, _currentGridModel.columnCount];
            
            SpawnColums();
            
            SpawnCell();
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    var coin = _coinController.SpawnCoin(i);
                    Vector3 spawnPoint = new Vector3(0, j * -2.5f, 0);
                    coin.CoinPosition.position = spawnPoint;
                    coin.CellTransformCellPosition(spawnPoint);
                    coin.transform.SetParent(_playingFieldView.CoinSpawPoint[i].transform, false);
                }
            }
        }

        public void SetActiveColums(bool value)
        {
            foreach (var colum in _columViewsList)
            {
                colum.BoxCollider.enabled = value;
            }
        }

        public void SpinColum()
        {
            CurrentColum.transform.DORotate(new Vector3(720, 0, 0), 1f, RotateMode.LocalAxisAdd)
                .OnComplete(()=>
                {
                    СheckAllCells();
                    _playingFieldController.SetActiveCoins(true);
                    OnSpinningIsDone?.Invoke();
                });
        }

        private void СheckAllCells()
        {
            CheckLines();
            CheckColums();
            CheckRightDiagonals();
            CheckLeftDiagonals();
        }

        private void CheckLeftDiagonals()
        {
            int currentCoinLeght = 0;
          
            for (int i = _currentGridModel.lineCount-1; i >= _currentGridModel.CountCellsToWin; i--)
            {
                int j = _currentGridModel.lineCount - i;
                    
                var coin = _cellViews[i, j].GetComponentInChildren<CoinView>();
                if (coin)
                {
                    currentCoinLeght++;
                    if (currentCoinLeght == _currentGridModel.CountCellsToWin)
                    {
                        Debug.Log("Win");
                    }
                }
                else
                {
                    currentCoinLeght = 0;
                    Debug.Log("Loose");
                }
                
            }
            
            currentCoinLeght = 0;
            
            for (int f = 0; f < _currentGridModel.columnCount; f++)
            {
                var offset = f;
                
                for (int i = _currentGridModel.lineCount - 1-offset; i >= 0; i--)
                {
                    int j = _currentGridModel.lineCount - 1 - i- offset;
                    
                    if (j < _currentGridModel.columnCount)
                    {
                        var coin = _cellViews[i, j].GetComponentInChildren<CoinView>();
                        if (coin)
                        {
                            currentCoinLeght++;
                            if (currentCoinLeght == _currentGridModel.CountCellsToWin)
                            {
                                Debug.Log("Win");
                            }
                        }
                        else
                        {
                            currentCoinLeght = 0;
                            Debug.Log("Loose");
                        }
                    }
                }
            }
        }

        private void CheckRightDiagonals()
        { 
            int currentCoinLeght = 0;
            
            for (int i = 0 ; i < _currentGridModel.columnCount-1; i++)
            {
                int j = i + 1;
                var coin = _cellViews[i,j].GetComponentInChildren<CoinView>();
                if (coin)
                {
                    currentCoinLeght++;
                    if (currentCoinLeght == _currentGridModel.CountCellsToWin)
                    {
                        Debug.Log("Win");
                    }
                }
                else
                {
                    currentCoinLeght = 0;
                    Debug.Log("Loose");
                }
            }
            
            currentCoinLeght = 0;
            
            for (int f = 0; f < _currentGridModel.columnCount; f++)
            {
                var offset = f;
                for (int i = 0 ; i < _currentGridModel.columnCount; i++)
                {
                    int j = i + offset;
                    if (j <= _currentGridModel.lineCount-1)
                    {
                        var coin = _cellViews[j,i].GetComponentInChildren<CoinView>();
                        if (coin)
                        {
                            currentCoinLeght++;
                            if (currentCoinLeght == _currentGridModel.CountCellsToWin)
                            {
                                Debug.Log("Win");
                            }
                        }
                        else
                        {
                            currentCoinLeght = 0;
                            Debug.Log("Loose");
                        }
                    }
                } 
            }
        }

        private void CheckColums()
        {
            for (int i = 0; i < _currentGridModel.columnCount; i++)
            {
                int currentCoinLeght = 0;
                for (int j = 0; j < _currentGridModel.lineCount; j++)
                {
                    var coin = _cellViews[j, i].GetComponentInChildren<CoinView>();
                    if (coin)
                    {
                        currentCoinLeght++;
                        if (currentCoinLeght == _currentGridModel.CountCellsToWin)
                        {
                            Debug.Log("Win");
                        }
                    }
                    else
                    {
                        currentCoinLeght = 0;
                        Debug.Log("Loose");
                    }
                }
            }
        }

        private void CheckLines()
        {
            for (int j = 0; j < _currentGridModel.lineCount; j++)
            {
                int currentCoinLeght = 0;
                for (int i = 0; i < _currentGridModel.columnCount; i++)
                {
                    var coin = _cellViews[j,i].GetComponentInChildren<CoinView>();
                    if (coin)
                    {
                        currentCoinLeght++;
                        if (currentCoinLeght == _currentGridModel.CountCellsToWin)
                        {
                            Debug.Log("Win");
                        }
                    }
                    else
                    {
                        currentCoinLeght = 0;
                        Debug.Log("Loose");
                    }
                }
            }
        }

        private void SpawnCell()
        {
            for (int i = 0; i < _currentGridModel.columnCount; i++)
            {
                List<CellView> someCurrentCells = new List<CellView>();

                for (int j = 0; j < _currentGridModel.lineCount; j++)
                {
                    var cellPosition = _firstCellPosition + 3.8f * new Vector3(0, j, 0);
                    var newCell = _cellViewPool.Spawn();

                    newCell.transform.position = cellPosition;
                    _cellViewsList.Add(newCell);
                    _cellViews[j, i] = newCell;
                    
                    someCurrentCells.Add(newCell);
                }

                foreach (var cell in someCurrentCells)
                {
                    cell.transform.SetParent(_columViewsList[i].transform, false);
                }
            }
        }

        private void SpawnColums()
        {
            for (int i = 0; i < _currentGridModel.columnCount; i++)
            {
                var columPosition = _firstColumPosition + 3.8f * new Vector3(i, 0, 0);
                var colum = _columViewPool.Spawn();
                colum.ColumTranform.position = columPosition;
                _columViewsList.Add(colum);
            }
            foreach (var colum in _columViewsList)
            {
                colum.transform.SetParent(_playingFieldView.Colums, false);
            }
        }
    }
}