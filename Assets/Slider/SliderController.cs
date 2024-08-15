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
        private bool _startBoolValue;
        private Tween _animationTween;
        
        public SliderController()
        {
            
        }

        public void SetSliderView(SliderView view)
        {
            _sliderView = view;
        }

        public SliderView GetSliderView()
        {
            return _sliderView;
        }
        
        public void ShowAnimation(bool value)
        {
            if (value)
            {
                InitButtons();
                SetStartValue();
                var rect = _sliderView.GetComponent<RectTransform>();
                
                rect.DOAnchorPos(_sliderView.Positions[0], _animationTime)
                    .OnComplete(() => StartSliding(_startBoolValue));
            }
            else
            {
                var rect = _sliderView.GetComponent<RectTransform>();
                UnSubscribeButtons();
                rect.DOAnchorPos(_sliderView.Positions[1], _animationTime);
            }
        }

        public void StartSliding(bool value)
        {
          _animationTween.Kill();
          _animationTween = null;
          
          if (value)
          {
              _animationTween = _sliderView.SliderImage.DOFillAmount(1, 1)
                  .OnComplete(() => StartSliding(!value));
          }
          else
          {
              _animationTween = _sliderView.SliderImage.DOFillAmount(0, 1)
                  .OnComplete(() => StartSliding(!value));
          }
        }

        public float GetSliderValue()
        {
            if (_currentSliderValue < 1)
            {
                _currentSliderValue = Random.Range(2,4);
            }
            if (_currentSliderValue == 10)
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
            _currentSliderValue = _sliderView.SliderImage.fillAmount * 10;
            _animationTween.Kill();
            _animationTween = null;
            
            ShowAnimation(false);
            OnGetReadyToSpin?.Invoke();
            
            UnSubscribeButtons();
        }
        
        private void UnSubscribeButtons()
        {
            _sliderView.UIButtons[0].OnClick -= CheckSliderValue;
        }

        private void SetStartValue()
        {
            _startFloatValue = 2.5f;
            _sliderView.SliderImage.fillAmount = _startFloatValue/5;
        }
    }
}