using DragController;
using Grid;
using UnityEngine;

namespace GameController
{
    public class GameController
    {
        private readonly IDragController _dragController;
        private readonly GridController _gridController;

        public GameController(
            IDragController dragController,
            GridController gridController)
        {
            _dragController = dragController;
            _gridController = gridController;
        }

        public void StartGame()
        {
            _gridController.SpawnGrid();
            _dragController.StartRaycastInteraction();
            Debug.Log("StartGame");
        }
    }
}