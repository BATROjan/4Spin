using Grid;
using UnityEngine;

namespace GameController
{
    public class GameController
    {
        private readonly GridController _gridController;

        public GameController(GridController gridController)
        {
            _gridController = gridController;
        }

        public void StartGame()
        {
            _gridController.SpawnGrid();
            Debug.Log("StartGame");
        }
    }
}