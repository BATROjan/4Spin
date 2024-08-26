using System;
using DG.Tweening;
using Grid.Cell;

namespace Environment
{
    public class EnvironmentController
    {
        public Action<CoinView> OnNexStep;

        public void NextStep(CoinView coin, CellView cellView)
        {
            coin.transform.DOMove(cellView.transform.position, 2).OnComplete(() =>
            {
                cellView.GetCollier().enabled = true;
                OnNexStep?.Invoke(coin);
            });
        }
    }
}