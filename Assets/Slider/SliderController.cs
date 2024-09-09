using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Slider
{
    public class SliderController
    {
        public Action OnGetReadyToSpin;
        
        private SliderView _sliderView;
        private float _animationTime = 0.2f;
        
        private float _startFloatValue;
        private float _currentSliderValue;
        private float _rotateAnimationTime = 1;

        private int _rotateAngle = 180;
        private Vector3 _startAngle = new Vector3(0,0,90);
        private bool _startBoolValue;
        
        private Tween _arrowAnimationTween;
        private Tween _sliderAnimationTween;
        
        public void SetSliderView(SliderView view)
        {
            _sliderView = view;
        }
        
        public void ShowAnimation(bool value)
        {
            if (value)
            {
                InitButtons();
                PrepearPanel();
                
                _sliderView.RectTransforms.DOAnchorPos(_sliderView.Positions[0], _animationTime)
                    .OnComplete(() => StartSliding(_startBoolValue));
            }
            else
            {
                UnSubscribeButtons();
                _sliderView.RectTransforms.DOAnchorPos(_sliderView.Positions[1], _animationTime);
            }
        }

        public int GetEmploymentValueSelect()
        {
            return Random.Range(2, 11);
        }

        public void StartSliding(bool value)
        {
          _arrowAnimationTween.Kill();
          _sliderAnimationTween.Kill();
          
          _arrowAnimationTween = null;
          _sliderAnimationTween = null;

          if (value)
          {
              _sliderAnimationTween = _sliderView.InvisSliderImage
                  .DOFillAmount(0, _rotateAnimationTime).SetEase(Ease.Linear);
              
              _arrowAnimationTween = _sliderView.SliderArrow.transform
                  .DOLocalRotate(new Vector3(0,0,180), _rotateAnimationTime, RotateMode.LocalAxisAdd)
                  .SetEase(Ease.Linear).OnComplete(() => StartSliding(!value));
          }
          else
          {
              _sliderAnimationTween = _sliderView.InvisSliderImage
                  .DOFillAmount(1, _rotateAnimationTime).SetEase(Ease.Linear);
              
              _arrowAnimationTween = _sliderView.SliderArrow.transform
                  .DOLocalRotate(new Vector3(0,0,-180), _rotateAnimationTime, RotateMode.LocalAxisAdd)
                  .SetEase(Ease.Linear).OnComplete(() => StartSliding(!value));
          }
        }

        public float GetSliderValue()
        {
            if (_currentSliderValue < 1)
            {
                _currentSliderValue = Random.Range(2,4);
            }
            if (_currentSliderValue > 9)
            {
                _currentSliderValue = Random.Range(9, 11);
            }
            
            return _currentSliderValue;
        }

        private void InitButtons()
        {
            _sliderView.UIButtons[0].OnClick += CheckSliderValue;
        }

        private void CheckSliderValue()
        {
            _currentSliderValue = _sliderView.InvisSliderImage.fillAmount * 10;
            
            _arrowAnimationTween.Kill();
            _sliderAnimationTween.Kill();
          
            _arrowAnimationTween = null;
            _sliderAnimationTween = null;
            
            ShowAnimation(false);
            OnGetReadyToSpin?.Invoke();
            
            UnSubscribeButtons();
        }
        
        private void UnSubscribeButtons()
        {
            _sliderView.UIButtons[0].OnClick -= CheckSliderValue;
        }

        private void PrepearPanel()
        {
            var rotation = _sliderView.SliderArrow.transform.rotation;
            rotation.eulerAngles = _startAngle;
            _sliderView.SliderArrow.transform.rotation = rotation;

            _sliderView.InvisSliderImage.fillAmount = 0;
        }
    }
}