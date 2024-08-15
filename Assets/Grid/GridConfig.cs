using System;
using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    [CreateAssetMenu(fileName = "GridConfig", menuName = "Configs/GridConfig")]

    public class GridConfig : ScriptableObject
    {
        [SerializeField] private float offset;
        [SerializeField] private GridModel[] gridModels;

        private Dictionary<DiffcultLevel, GridModel> _dictionaryOfLevels = new();
        private bool _isInit;

        public GridModel GetGrid(DiffcultLevel diffcultLevel)
        {
            if (!_isInit)
            {
                Init();
            }

            if (_dictionaryOfLevels.TryGetValue(diffcultLevel, out var levelModel))
            {
                return levelModel;
            }
            throw new Exception($"{nameof(diffcultLevel)}: {diffcultLevel} doesn't contains in dictionary");
        }

        private void Init()
        {
            foreach (var gridModel in gridModels)
            {
                if (!_dictionaryOfLevels.TryAdd(gridModel.diffcultLevel, gridModel))
                {
                    Debug.LogError($"{gridModel.diffcultLevel} doesn't add to dictionary");
                }
            }
            _isInit = true;
        }
    }
}

[Serializable]
public struct GridModel
{
    public DiffcultLevel diffcultLevel;
    public int columnCount;
    public int lineCount;
    public int CountCellsToWin;
}

public enum DiffcultLevel
{
    Easy,
    Normal,
    Hard
}