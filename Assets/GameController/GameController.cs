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
        private readonly IUIService _uiService;
        private readonly IDragController _dragController;
        private readonly GridController _gridController;

        public GameController(
            IUIService uiService,
            IDragController dragController,
            GridController gridController)
        {
            _uiService = uiService;
            _dragController = dragController;
            _gridController = gridController;
        }

        public void StartGame()
        {
            _gridController.SpawnGrid();
            _dragController.StartRaycastInteraction();
            Debug.Log("StartGame");
        }

        public void RestartGame()
        {
            _uiService.Hide<UIWinWindowView>();
            _uiService.Show<UIPlayingWindowView>();
            _gridController.DespawnCoins();
            _gridController.SpawnCoins();
        }
    }
}