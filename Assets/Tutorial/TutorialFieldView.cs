using UnityEngine;
using Zenject;

namespace Tutorial
{
    public class TutorialFieldView : MonoBehaviour
    {
        public ColumVew[] ColumVews => _columVews;

        [SerializeField] private ColumVew[] _columVews;
        [SerializeField] private GameObject[] fields;

        private GameObject _currentView;
        
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
            Destroy(_currentView);
            _currentView = null;
        }
        public class  Pool : MonoMemoryPool<DiffcultLevel,TutorialFieldView>
        {
            protected override void Reinitialize(DiffcultLevel diffcultLevel, TutorialFieldView item)
            {
                item.InstantiateView(diffcultLevel);
            }
        }
    }
}