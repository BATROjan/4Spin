using System.Collections;
using System.Collections.Generic;
using Coin;
using UnityEngine;

public class CoinController 
{
   private readonly CoinView.Pool _coinPool;
   private readonly CoinConfig _coinConfig;
   
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
      SetActiveCollider(coinView, true);
      return coinView;
   }

   public void SetActiveCollider(CoinView coinView, bool value)
   {
      coinView.GetMeshCollider().enabled = value;
   }

   public void DespawnCoin(CoinView coin)
   {
      _coinPool.Despawn(coin);
   }
}
