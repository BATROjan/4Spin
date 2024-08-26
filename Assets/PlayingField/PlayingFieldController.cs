using System;
using DG.Tweening;
using Unity.VisualScripting;

namespace PlayingField
{
    public class PlayingFieldController
    {
        public Action NextStep;
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

        public void SetActiveCoin(bool value, bool isFirstPlayer = false, bool isPvE = false)
        {
            if (value)
            {
                _playingFieldView.CurrentCoinPoint.transform.DOLocalMove(_playingFieldView.CurrentCoinPoint.GetShowPosition(), _animationTime).OnComplete(
                    () =>
                    {
                        if (!isFirstPlayer && isPvE)
                        {
                            NextStep?.Invoke();
                        }
                      });
            }
            else
            {
                _playingFieldView.CurrentCoinPoint.transform.DOLocalMove(_playingFieldView.CurrentCoinPoint.GetHidePosition(), _animationTime);
            }
        }
    }
}