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

        public void DespawnView()
        {
            _playingFieldViewPool.Despawn(_playingFieldView);
        }

        public void SetActiveCoin(bool value)
        {
            if (value)
            {
                _playingFieldView.CurrentCoinPoint.transform.DOLocalMove(_playingFieldView.CurrentCoinPoint.GetShowPosition(), _animationTime);
            }
            else
            {
                _playingFieldView.CurrentCoinPoint.transform.DOLocalMove(_playingFieldView.CurrentCoinPoint.GetHidePosition(), _animationTime);
            }
        }
    }
}