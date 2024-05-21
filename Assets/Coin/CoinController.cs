using System.Collections;
using System.Collections.Generic;
using Coin;
using UnityEngine;

public class CoinController 
{
   private readonly CoinView.Pool _coinPool;
   private readonly CoinConfig _coinConfig;
   
   private List<CoinView> _coinViews = new List<CoinView>();
   
   public CoinController(CoinView.Pool coinPool,
      CoinConfig coinConfig)
   {
      _coinPool = coinPool;
      _coinConfig = coinConfig;
   }

   public CoinView SpawnCoin(int id)
   {
      var coinView = _coinPool.Spawn(_coinConfig.GetCoinModel(id));
      coinView.CellTransformCellPosition(coinView.transform.position);
      _coinViews.Add(coinView);
      return coinView;
   }

   public void DespawnCoin()
   {
      foreach (var coin in _coinViews)
      {
         _coinPool.Despawn(coin);
      }   
      
      _coinViews.Clear();
   }
}
