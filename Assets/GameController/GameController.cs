using DG.Tweening;
using DragController;
using Grid;
using UI.UIPlayingWindow;
using UI.UIService;
using UI.UIWinWindow;
using UnityEngine;

namespace GameController
{
    public class GameController
    {
        private readonly GameConfig _gameConfig;
        private readonly IUIService _uiService;
        private readonly IDragController _dragController;
        private readonly GridController _gridController;
        
        public GameController(
            GameConfig gameConfig,
            IUIService uiService,
            IDragController dragController,
            GridController gridController)
        {
            _gameConfig = gameConfig;
            _uiService = uiService;
            _dragController = dragController;
            _gridController = gridController;
        }

        public void StartGame()
        {
            _gridController.CheckIsPvE(_gameConfig.IsPvE);
            _gridController.SpawnGrid();
            
            _gridController.SpawnCoins();
            _gridController.PikupCoin();
            
            _dragController.StartRaycastInteraction();
        }

        public void RestartGame()
        { 
            _gridController.CheckIsPvE(_gameConfig.IsPvE);
            
            _uiService.Hide<UIWinWindowView>();
            _uiService.Show<UIPlayingWindowView>();
            
            _gridController.DespawnAll();
            
            _gridController.ClearAll();
            
                _gridController.SpawnGrid();
                _gridController.SpawnCoins();
            
                _gridController.ResetAll();
            
                _gridController.PikupCoin();
           
        }

        public void SetPvE(bool value)
        {
            _gameConfig.IsPvE = value;
        }
    }
}