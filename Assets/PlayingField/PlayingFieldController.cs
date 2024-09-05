using System;
using DG.Tweening;
using Unity.VisualScripting;

namespace PlayingField
{
    public class PlayingFieldController
    {
        public Action NextStep;
        public Action OnAnimationEnd;

        public BackSpriteView BackSpriteView => _backSpriteView;
        
        private readonly BackSpriteConfig _backSpriteConfig;
        private readonly BackSpriteView.Pool _backSpritePool;
        private readonly PlayingFieldView.Pool _playingFieldViewPool;
        private readonly PlayingFieldConfig _playingFieldConfig;

        private PlayingFieldView _playingFieldView;
        private BackSpriteView _backSpriteView;
        private float _animationTime = 0.7f;
        
        private Tween _blackTween;
        public PlayingFieldController(
            BackSpriteConfig backSpriteConfig,
            BackSpriteView.Pool backSpritePool,
            PlayingFieldView.Pool playingFieldViewPool,
            PlayingFieldConfig playingFieldConfig)
        {
            _backSpriteConfig = backSpriteConfig;
            _backSpritePool = backSpritePool;
            _playingFieldViewPool = playingFieldViewPool;
            _playingFieldConfig = playingFieldConfig;
            
            _backSpriteConfig.Init();
        }

        public PlayingFieldView SpawnPlayingVew(DiffcultLevel diffcultLevel)
        {
            PlayingFieldModel model = _playingFieldConfig.GetField(diffcultLevel);
            var view = _playingFieldViewPool.Spawn(model);
            
            view.CurrentCoinPoint.transform.localPosition = model.CurrentCoinPosition;
            view.CurrentCoinPoint.ChangeShowPosition(model.CurrentCoinPosition);
            view.transform.position = model.FieldPosition;
            _playingFieldView = view;
            
            return view;
        }

        public BackSpriteView SpawnBackSpriteView()
        {
             return _backSpriteView = _backSpritePool.Spawn();
        }

        public void ChangeBackSpritePosition(BackSpriteType type)
        {
            _backSpriteView.transform.DOMove(_backSpriteConfig.GetSpritePosition(type), 0.7f).OnComplete(()=> OnAnimationEnd?.Invoke());;
        }

        public void ActivateBlackBackground(bool value)
        {
            _blackTween.Kill();
            _blackTween = null;
            
            if (value)
            {
                _blackTween = _backSpriteView.BlackSprite.DOFade(0.8f, 1);
            }
            else
            {
                _blackTween = _backSpriteView.BlackSprite.DOFade(0, 0.5f);
            }
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