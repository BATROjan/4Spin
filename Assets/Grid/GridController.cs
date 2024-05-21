using System;
using System.Collections.Generic;
using DG.Tweening;
using Grid.Cell;
using PlayingField;
using Slider;
using UI;
using UI.UIPlayingWindow;
using UI.UIService;
using UI.UIWinWindow;
using Unity.VisualScripting;
using UnityEngine;
using Random = System.Random;

namespace Grid
{
    public class  GridController
    {
        public ColumVew CurrentColum;
        public Action OnSpinningIsDone;

        private readonly IUIService _uiService;
        private readonly SliderController _sliderController;
        private readonly CoinController _coinController;
        private readonly PlayingFieldController _playingFieldController;
        private readonly ColumVew.Pool _columViewPool;
        private readonly CellView.Pool _cellViewPool;
        private readonly GridConfig _gridConfig;

        private List<CellView> _cellViewsList = new List<CellView>();
        private List<ColumVew> _columViewsList = new List<ColumVew>();

        private Dictionary<int, ColumVew> _dictionaryOfColums = new Dictionary<int, ColumVew>();
        private Dictionary<ColumVew, List<CellView>> _dictionaryOfCells = new Dictionary<ColumVew, List<CellView>>();
        
        private Vector3 _firstColumPosition = new Vector3(-5.6f,0,0);
        private Vector3 _firstCellPosition = new Vector3(0,-9.58f,0);

        private GridModel _currentGridModel;
        private CellView[,] _cellViews;

        private UIWinWindowView _uiWinWindowView;
        private PlayingFieldView _playingFieldView;

        private int _currentNumberComand;
        private int _currentCoinLeght = 0;
        
        public GridController(
            IUIService uiService,
            SliderController sliderController,
            CoinController coinController,
            PlayingFieldController playingFieldController,
            ColumVew.Pool columViewPool,
            CellView.Pool cellViewPool,
            GridConfig gridConfig
        )
        {
            _uiService = uiService;
            _sliderController = sliderController;
            _coinController = coinController;
            _playingFieldController = playingFieldController;
            _columViewPool = columViewPool;
            _cellViewPool = cellViewPool;
            _gridConfig = gridConfig;

            _sliderController.OnGetReadyToSpin += SpinColum;

            _uiWinWindowView = _uiService.Get<UIWinWindowView>();
        }

        public void SpawnGrid()
        { 
            _currentGridModel = _gridConfig.GetGrid(DiffcultLevel.Normal);
            
            var playingFieldView = _playingFieldController.SpawnPlayingVew(_currentGridModel.diffcultLevel);
            _playingFieldView = playingFieldView;
            
            _cellViews = new CellView[_currentGridModel.lineCount, _currentGridModel.columnCount];
            
            SpawnColums();
            SpawnCell();
            
            SpawnCoins();
        }

        public void SpawnCoins()
        {
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

        public void ClearAll()
        {
            ClearDictionarys();
        }

        public void DespawnCoins()
        {
           _coinController.DespawnCoin(); 
        }

        private void ClearDictionarys()
        {
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
            int xValue = (int)_sliderController.GetSliderValue()*180;
            if ((int)_sliderController.GetSliderValue() % 2 != 0)
            {
                FlipColumn(CurrentColum.ColumID);
            }
    
            Debug.Log("Random Angle = " + xValue);
            CurrentColum.transform.DORotate(new Vector3(xValue, 0, 0), 3f, RotateMode.LocalAxisAdd)
                .OnComplete(()=>
                {
                    СheckAllCells();
                    _playingFieldController.SetActiveCoins(true);
                    OnSpinningIsDone?.Invoke();
                });
        }
        
        public void FlipColumn(int columnIndex)
        {
            int linesCount = _cellViews.GetLength(0);
            
            for (int i = 0; i < linesCount / 2; i++)
            {
                CellView temp = _cellViews[i, columnIndex];
                _cellViews[i, columnIndex] = _cellViews[linesCount - i - 1, columnIndex];
                _cellViews[linesCount - i - 1, columnIndex] = temp;
            }
        }

        private void СheckAllCells()
        {
            CheckLines();
            CheckColums();
            CheckRightDiagonals();
            CheckLeftDiagonals();
        }

        private void CheckLines()
        {
            for (int j = 0; j < _currentGridModel.lineCount; j++)
            {
                _currentCoinLeght = 0;
                for (int i = 0; i < _currentGridModel.columnCount; i++)
                {
                    CheckCell(j, i);
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
                    CheckCell(j, i);
                }
            }
        }

        private void CheckRightDiagonals()
        { 
            int currentCoinLeght = 0;
            
            for (int j = 0 ; j < _currentGridModel.columnCount-1; j++)
            {
                int i = j + 1;
                CheckCell(j, i);
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
                        CheckCell(j, i);
                    }
                } 
            }
        }

        private void CheckLeftDiagonals()
        {
            int currentCoinLeght = 0;
          
            for (int j = _currentGridModel.lineCount-1; j >= _currentGridModel.CountCellsToWin; j--)
            {
                int i = _currentGridModel.lineCount - j;

                CheckCell(j, i);
            }
            
            currentCoinLeght = 0;
            
            for (int f = 0; f < _currentGridModel.columnCount; f++)
            {
                var offset = f;
                
                for (int j = _currentGridModel.lineCount - 1-offset; j >= 0; j--)
                {
                    int i = _currentGridModel.lineCount - 1 - j- offset;
                    
                    if (i < _currentGridModel.columnCount)
                    {
                        CheckCell(j, i);
                    }
                }
            }
        }

        private void CheckCell(int j, int i)
        {
            var coin = _cellViews[j, i].GetComponentInChildren<CoinView>();
            if (coin)
            {
                if (coin.NumberComand == _currentNumberComand)
                {
                    _currentCoinLeght++;
                    if (_currentCoinLeght == _currentGridModel.CountCellsToWin)
                    { 
                        _uiService.Show<UIWinWindowView>();
                        _uiService.Hide<UIPlayingWindowView>();
                        var text = "Win " + coin.NumberComand + " Comand";
                        _uiWinWindowView.WinText.text = text;
                        Debug.Log("Win" + coin.NumberComand + " comand");
                    }
                }
                if (coin.NumberComand != _currentNumberComand)
                {
                    _currentCoinLeght = 1;
                    _currentNumberComand = coin.NumberComand;
                }
            }
            else
            {
                _currentCoinLeght = 0;
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
                
                _dictionaryOfCells.Add(_dictionaryOfColums[i], someCurrentCells);

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
                colum.ColumID = i;
                colum.ColumTranform.position = columPosition;
                _dictionaryOfColums.Add(i, colum);
                _columViewsList.Add(colum);
            }
            foreach (var colum in _columViewsList)
            {
                colum.transform.SetParent(_playingFieldView.Colums, false);
            }
        }
    }
}