using System;
using System.Collections.Generic;
using DG.Tweening;
using Environment;
using Grid.Cell;
using PlayingField;
using Slider;
using UI.UIPlayingWindow;
using UI.UIService;
using UI.UIWinWindow;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Grid
{
    public class  GridController
    {
        public Action OnLastCoinIsSet;
        public Action OnSpinningIsDone;

        private readonly AudioController.AudioController _audioController;
        private readonly EnvironmentController _environmentController;
        private readonly IUIService _uiService;
        private readonly SliderController _sliderController;
        private readonly CoinController _coinController;
        private readonly PlayingFieldController _playingFieldController;
        private readonly ColumVew.Pool _columViewPool;
        private readonly CellView.Pool _cellViewPool;
        private readonly GridConfig _gridConfig;

        private List<CellView> _freeCellViewsList = new List<CellView>();
        private List<CellView> _cellViewsList = new List<CellView>();
        private List<CellView> _lightsCellViewsList = new List<CellView>();
        private List<ColumVew> _columViewsList = new List<ColumVew>();
        private List<CoinView> _coinViewsList = new List<CoinView>();

        private Dictionary<int, ColumVew> _dictionaryOfColums = new Dictionary<int, ColumVew>();
        private Dictionary<int, Queue<CoinView>> _dictionaryOfCoins = new Dictionary<int, Queue<CoinView>>();
        private Dictionary<ColumVew, List<CellView>> _dictionaryOfCells = new Dictionary<ColumVew, List<CellView>>();

        private Vector3 _firstColumPosition = new Vector3(-5.6f,0,0);
        private Vector3 _firstCellPosition = new Vector3(0,-9.58f,0);

        private GridModel _currentGridModel;
        private CellView[,] _cellViews;

        private ColumVew _currentColum;
        private UIWinWindowView _uiWinWindowView;
        private PlayingFieldView _playingFieldView;
        private CoinView _currentCoinView;

        private int _currentNumberComand;
        private int _currentCoinLeght = 0;
        
        private bool isPvE;
        private bool isFistPlayer = true;
        private bool isWin;

        private Tween rotationTween;
        public GridController(
            AudioController.AudioController audioController,
            EnvironmentController environmentController,
            IUIService uiService,
            SliderController sliderController,
            CoinController coinController,
            PlayingFieldController playingFieldController,
            ColumVew.Pool columViewPool,
            CellView.Pool cellViewPool,
            GridConfig gridConfig
        )
        {
            _audioController = audioController;
            _environmentController = environmentController;
            _uiService = uiService;
            _sliderController = sliderController;
            _coinController = coinController;
            _playingFieldController = playingFieldController;
            _columViewPool = columViewPool;
            _cellViewPool = cellViewPool;
            _gridConfig = gridConfig;

            _sliderController.OnGetReadyToSpin += SpinColum;
            _playingFieldController.NextStep += EmploymentStep;
            _gridConfig.Init();
            
            _uiWinWindowView = _uiService.Get<UIWinWindowView>();
        }

        public void SpawnGrid(DiffcultLevel diffcultLevel)
        { 
            _currentGridModel = _gridConfig.GetGrid(diffcultLevel);
            
            var playingFieldView = _playingFieldController.SpawnPlayingVew(_currentGridModel.diffcultLevel);
            _playingFieldView = playingFieldView;
            
            _cellViews = new CellView[_currentGridModel.lineCount, _currentGridModel.columnCount];
            
            SpawnColums();
            SpawnCell();
        }

        public void CheckIsPvE(bool value)
        {
            isPvE = value;
        }

        public void PikupCoin()
        {
            if (!isFistPlayer)
            {
                _currentCoinView = _dictionaryOfCoins[1].Peek();
                _dictionaryOfCoins[1].Dequeue();
                if (isPvE)
                {
                    _currentCoinView.gameObject.layer = 3;
                }
            }
            else
            {
                _currentCoinView = _dictionaryOfCoins[0].Peek();
                _dictionaryOfCoins[0].Dequeue();
            }
            
            if (_dictionaryOfCoins[0].Count == 0 && _dictionaryOfCoins[1].Count == 0)
            {
                OnLastCoinIsSet += Draw;
            }
            
            isFistPlayer = !isFistPlayer;

            _currentCoinView.transform.SetParent(_playingFieldView.CurrentCoinPoint.transform, false);
            
            _currentCoinView.transform.localPosition = Vector3.zero;
        }

        public void ResetAll()
        {
            SetActiveCellCollider(true);
            ResetPlayerStep();
            ResetWinBoolValue(false);
        }

        private void Draw()
        {
            _uiService.Show<UIWinWindowView>();
            _uiService.Hide<UIPlayingWindowView>();
            _uiWinWindowView.WinText.text = "Draw";
        }

        public void SetActiveCoinCollider(CoinView coinView, bool value)
        {
            _coinController.SetActiveCollider(coinView, value);
        }

        public void ResetCoinView()
        {
            _currentCoinView = null;
        }

        public void SetActiveCellCollider(bool value)
        {
            foreach (var cell in _freeCellViewsList)
            {
                cell.GetCollier().enabled = value;
            }
        }

        public void ResetPlayerStep()
        {
            isFistPlayer = true;
        }

        public void ResetWinBoolValue(bool value)
        {
            isWin = value;
        }

        public void SetCollorToCell(int id)
        {
            foreach (var cell in _lightsCellViewsList)
            {
                cell.GetMeshRenderer().material = _gridConfig.GetMaterial(id);
            }
        }

        public void SpawnCoins()
        {
            int needCoinsCount = CurrentCoinsCount();
            
            for (int i = 0; i < 2; i++)
            {
                Queue<CoinView> coinViews = new Queue<CoinView>();
                for (int j = 0; j < needCoinsCount; j++)
                {
                    var coin = _coinController.SpawnCoin(i);
                    Vector3 spawnPoint = new Vector3(0, j * -2.5f, 0);
                    
                    coin.CoinPosition.position = spawnPoint;
                    coin.CellTransformCellPosition(Vector3.zero);
                    
                    coin.OnCellFill += SelectColum;
                    coin.transform.SetParent(_playingFieldView.CoinSpawPoint[i].transform, false);
                  
                    _coinViewsList.Add(coin);
                    coinViews.Enqueue(coin);
                }
                _dictionaryOfCoins.Add(i, coinViews);
            }
        }

        private int CurrentCoinsCount()
        {
            int needCoins =
                _gridConfig.GetGrid(DiffcultLevel.Normal).columnCount *
                _gridConfig.GetGrid(DiffcultLevel.Normal).lineCount / 2;
            return needCoins;
        }

        public void ClearAll()
        {
            ClearDictionarys();
            ClearLists();
        }

        private void ClearDictionarys()
        {
            _dictionaryOfCoins.Clear();
            _dictionaryOfColums.Clear();
            _dictionaryOfCells.Clear();
        }

        private void ClearLists()
        {
            _coinViewsList.Clear();
            _columViewsList.Clear();
            _freeCellViewsList.Clear();
            _cellViewsList.Clear();
        }

        public void DespawnAll()
        {
            DespawnCoins();
            DespawnColums();
            DespawCells();
            DespawnPlayingView();
            
            rotationTween.Kill();
            rotationTween = null;
            
            OnSpinningIsDone?.Invoke();
        }

        public void RemoveCelFromList(CellView view)
        {
            _freeCellViewsList.Remove(view);
        }

        private void DespawnPlayingView()
        {
            _playingFieldController.DespawnView();
            _playingFieldView = null;
        }

        private void DespawCells()
        {
            foreach (var cell in _cellViewsList)
            {
                _cellViewPool.Despawn(cell);
            }
        }

        private void DespawnCoins()
        {
            foreach (var coin in _coinViewsList)
            {
                coin.OnCellFill -= SelectColum;
                _coinController.DespawnCoin(coin); 
            }
        }

        public void SpinColum()
        {
            int xValue = 0;
            
            if (isPvE && isFistPlayer)
            {
                xValue = _sliderController.GetEmploymentValueSelect()*180;
            }
            else
            { 
                xValue = (int)_sliderController.GetSliderValue()*180;
            }
            if (xValue % 360 != 0)
            {
                FlipColumn(_currentColum.ColumID);
            }
            rotationTween.Kill();
            rotationTween = null;
            var aaa = (float)xValue /180/ 11+0.1f;
            _audioController.Play(AudioType.Flip,aaa,true, true);
            
            rotationTween = _currentColum.transform.DORotate(new Vector3(xValue, 0, 0), 3f, RotateMode.LocalAxisAdd)
                .OnComplete(()=>
                {
                    СheckAllCells();
                    _playingFieldController.SetActiveCoin(true, isFistPlayer, isPvE);
                    OnSpinningIsDone?.Invoke();
                    if (_currentCoinLeght != _currentGridModel.CountCellsToWin && OnLastCoinIsSet != null)
                    {
                        OnLastCoinIsSet?.Invoke();
                    }

                    if (!isWin)
                    {
                        PikupCoin();
                    }
                });
        }

        private void EmploymentStep()
        {
            CellView cellView = GetRandomCellView();
            _environmentController.NextStep(_currentCoinView, cellView);
            _playingFieldController.SetActiveCoin(false);
            if (_currentCoinView)
            {
                RemoveCelFromList(cellView);
            }
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

        private CellView GetRandomCellView()
        {
            int id = Random.Range(0, _freeCellViewsList.Count - 1);

            return _freeCellViewsList[id];
        }

        private void СheckAllCells()
        {
            if (!isWin)
            {
                CheckLines();
            }
            if (!isWin)
            {
                CheckColums();
            }
            if (!isWin)
            { 
                CheckRightDiagonals();
            }
            if (!isWin)
            { 
                CheckLeftDiagonals();
            }
        }

        private void CheckLines()
        {
            for (int j = 0; j < _currentGridModel.lineCount; j++)
            {
                _currentCoinLeght = 0;
                _lightsCellViewsList.Clear();
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
                _currentCoinLeght = 0;
                _lightsCellViewsList.Clear();
                for (int j = 0; j < _currentGridModel.lineCount; j++)
                {
                    CheckCell(j, i);
                }
            }
        }

        private void CheckRightDiagonals()
        { 
            _currentCoinLeght = 0;
            _lightsCellViewsList.Clear();
            for (int j = 0 ; j < _currentGridModel.columnCount-1; j++)
            {
                int i = j + 1;
                CheckCell(j, i);
            }
            
            _lightsCellViewsList.Clear();
            for (int f = 0; f < _currentGridModel.columnCount; f++)
            {
                _currentCoinLeght = 0;
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
            _currentCoinLeght = 0;
            _lightsCellViewsList.Clear();
            
            for (int j = _currentGridModel.lineCount-1; j >= _currentGridModel.CountCellsToWin; j--)
            {
                int i = _currentGridModel.lineCount - j;

                CheckCell(j, i);
            }
            
            _lightsCellViewsList.Clear();
            for (int f = 0; f < _currentGridModel.columnCount; f++)
            {
                _currentCoinLeght = 0;
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
                if (!isWin)
                {

                    if (coin.NumberComand == _currentNumberComand)
                    {
                        _currentCoinLeght++;
                        _lightsCellViewsList.Add(_cellViews[j, i]);
                        if (_currentCoinLeght == _currentGridModel.CountCellsToWin)
                        {
                            SetCollorToCell(1);
                            isWin = true;
                            DOVirtual.DelayedCall(2, () =>
                            {
                                _uiService.Show<UIWinWindowView>();
                                _uiService.Hide<UIPlayingWindowView>();
                                var text = "Победа " + coin.NumberComand + " команды";
                                _uiWinWindowView.WinText.text = text;
                            });

                        }
                    }

                    if (coin.NumberComand != _currentNumberComand)
                    {
                        _lightsCellViewsList.Clear();
                        _lightsCellViewsList.Add(_cellViews[j, i]);
                        _currentCoinLeght = 1;
                        _currentNumberComand = coin.NumberComand;
                    }
                }
            }
            else
            {
                _currentCoinLeght = 0;
                _lightsCellViewsList.Clear();
            }
        }

        private void SpawnCell()
        {
            for (int i = 0; i < _currentGridModel.columnCount; i++)
            {
                List<CellView> someCurrentCells = new List<CellView>();

                for (int j = 0; j < _currentGridModel.lineCount; j++)
                {
                    var cellPosition = _currentGridModel.FirstCellPosionY + 3.8f * new Vector3(0, j, 0);
                    var newCell = _cellViewPool.Spawn(_gridConfig.GetMaterial(0));
                    newCell.transform.position = cellPosition;

                    _freeCellViewsList.Add(newCell);
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
        
        private void SelectColum(CellView cellView)
        {
            foreach (var colum in _dictionaryOfCells)
            {
                foreach (var cell in colum.Value)
                {
                    if (cell == cellView)
                    {
                        _currentColum = colum.Key;
                        
                        if (isPvE)
                        {
                            if (!isFistPlayer)
                            {
                                _sliderController.ShowAnimation(true);
                            }
                            else
                            {
                                SpinColum();
                            }
                        }
                        else
                        {
                            _sliderController.ShowAnimation(true);
                        }
                        break;
                    }
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

                var rotation = colum.ColumTranform.rotation;
                rotation.eulerAngles = Vector3.zero;
                colum.ColumTranform.rotation = rotation;

                _dictionaryOfColums.Add(i, colum);
                _columViewsList.Add(colum);
                
            }
            foreach (var colum in _columViewsList)
            {
                colum.transform.SetParent(_playingFieldView.Colums, false);
            }
        }

        private void DespawnColums()
        {
            foreach (var colum in _columViewsList)
            {
                _columViewPool.Despawn(colum);
            }
        }
    }
}