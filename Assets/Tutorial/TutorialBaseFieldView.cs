using UnityEngine;
using Zenject;

namespace Tutorial
{
    public class TutorialBaseFieldView : MonoBehaviour
    {
        public TutorialFieldView CurrentView => _currentView;
        
        [SerializeField] private TutorialFieldView[] fields;
        [SerializeField] private TutorialFieldView _currentView;
        
        public void InstantiateView(DiffcultLevel diffcultLevel)
        {
            if (_currentView)
            {
                Destroy(_currentView);
                _currentView = null;
            }
            switch (diffcultLevel)
            {
                case DiffcultLevel.Easy:
                    _currentView = Instantiate(fields[0]);
                    break;
                case DiffcultLevel.Normal :
                    _currentView = Instantiate(fields[1]);
                    break;
                case DiffcultLevel.Hard :
                    _currentView = Instantiate(fields[2]);
                    break;
            } 
            _currentView.transform.SetParent(transform, false);
        }

        public void DestoyView()
        {
            Destroy(_currentView.gameObject);
            _currentView = null;
        }
        public class  Pool : MonoMemoryPool<DiffcultLevel,TutorialBaseFieldView>
        {
            protected override void Reinitialize(DiffcultLevel diffcultLevel, TutorialBaseFieldView item)
            {
                item.InstantiateView(diffcultLevel);
            }
        }
    }
}