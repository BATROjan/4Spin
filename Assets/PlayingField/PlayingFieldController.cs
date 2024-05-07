using DG.Tweening;

namespace PlayingField
{
    public class PlayingFieldController
    {
        private readonly PlayingFieldView.Pool _playingFieldViewPool;
        private readonly PlayingFieldConfig _playingFieldConfig;

        private PlayingFieldView _playingFieldView;
        private float _animationTime = 0.7f;
        
        public PlayingFieldController(
            PlayingFieldView.Pool playingFieldViewPool,
            PlayingFieldConfig playingFieldConfig)
        {
            _playingFieldViewPool = playingFieldViewPool;
            _playingFieldConfig = playingFieldConfig;
        }

        public PlayingFieldView SpawnPlayingVew(DiffcultLevel diffcultLevel)
        {
            var view = _playingFieldViewPool.Spawn(_playingFieldConfig.GetField(DiffcultLevel.Normal));
            _playingFieldView = view;
            return view;
        }

        public void SetActiveArrows(bool value)
        {
            foreach (var arrow in _playingFieldView.GetArrows())
            {
                arrow.gameObject.SetActive(value);
            }
        }

        public void SetActiveCoins(bool value)
        {
            foreach (var spawnView in _playingFieldView.CoinSpawPoint)
            {
                if (!value)
                {
                    spawnView.transform.DOLocalMove(spawnView.GetShowPosition(), _animationTime);
                }
                else
                {
                    spawnView.transform.DOLocalMove(spawnView.GetHidePosition(), _animationTime);
  
                }
            }
        }
    }
}