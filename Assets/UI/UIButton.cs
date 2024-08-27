using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class UIButton : MonoBehaviour, IPointerClickHandler
    {
        public Action OnClick;
        public Action<DiffcultLevel> OnSelectLevel;
        
        [SerializeField] private Text buttonText;
        [SerializeField] private DiffcultLevel diffcultLevel;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke();
            OnSelectLevel?.Invoke(diffcultLevel);
        }
    }
}