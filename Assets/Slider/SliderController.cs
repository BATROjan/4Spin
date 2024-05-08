using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Slider
{
    public class SliderController
    {
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
                StartRandomValue();

                _sliderView.transform.DOLocalMove(_sliderView.Positions[0], _animationTime)
                    .OnComplete(() => StartSliding(_startBoolValue));
            }
            else
            {
                UnSubscribeButtons();
                _sliderView.transform.DOLocalMove(_sliderView.Positions[1], _animationTime);
            }
        }

        public void StartSliding(bool value)
        {
          _animationTween.Kill();
          _animationTween = null;
          
          if (value)
          {
              _animationTween = _sliderView.SliderImage.DOFillAmount(1, 0.5f)
                  .OnComplete(() => StartSliding(!value));
          }
          else
          {
              _animationTween = _sliderView.SliderImage.DOFillAmount(0, 0.5f)
                  .OnComplete(() => StartSliding(!value));
          }
        }

        private void InitButtons()
        {
            _sliderView.UIButtons[0].OnClick += CheckSliderValue;
        }

        private void CheckSliderValue()
        {
            _currentSliderValue = _sliderView.SliderImage.fillAmount;
            _animationTween.Kill();
            _animationTween = null;
            ShowAnimation(false);
            UnSubscribeButtons();
        }
        
        private void UnSubscribeButtons()
        {
            _sliderView.UIButtons[0].OnClick -= CheckSliderValue;
        }

        private void StartRandomValue()
        {
            _startFloatValue = Random.Range(0.0f, 1.0f);
            _sliderView.SliderImage.fillAmount = _startFloatValue;
            
            if (_startFloatValue>= 0.5f)
            {
                _startBoolValue = true;
            }
            else
            {
                _startBoolValue = false;
            }
        }
    }
}