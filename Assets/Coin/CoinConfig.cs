using System;
using UnityEngine;

namespace Coin
{
    [CreateAssetMenu(fileName = "CoinConfig", menuName = "Configs/CoinConfig")]

    public class CoinConfig : ScriptableObject
    {
        [SerializeField] private CoinModel[] coinModel;
        
        public CoinModel GetCoinModel(int id)
        {
             var currentCoinModel = coinModel[id];
             currentCoinModel.Material.color = currentCoinModel.Color;
             return currentCoinModel;
        }
    }

    [Serializable]
    public struct CoinModel
    {
        public Material Material;
        public Color Color;
    }
}