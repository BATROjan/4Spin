using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UIButton : MonoBehaviour, IPointerClickHandler
    {
        public Action OnClick;
        
        [SerializeField] private Text buttonText;
        
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
        }
    }
}