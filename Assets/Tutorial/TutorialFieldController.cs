using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Tutorial
{
    public class TutorialFieldController
    {
        public TutorialBaseFieldView TutorialBaseFieldView => _tutorialBaseFieldView;
        private readonly TutorialBaseFieldView.Pool _tutorialFielPool;

        private TutorialBaseFieldView _tutorialBaseFieldView;
        
        public TutorialFieldController(
            TutorialBaseFieldView.Pool tutorialFielPool)
        {
            _tutorialFielPool = tutorialFielPool;
        }

        public void Spawn(DiffcultLevel diffcultLevel)
        {
          _tutorialBaseFieldView = _tutorialFielPool.Spawn(diffcultLevel);
          
          _tutorialBaseFieldView.transform.localScale = Vector3.one;
          RotateColum();
        }
        public void DestroyField()
        {
            _tutorialBaseFieldView.DestoyView();
            _tutorialFielPool.Despawn(_tutorialBaseFieldView);
        }

        public void RotateColum()
        {
            if (_tutorialBaseFieldView)
            {
                float delay = Random.Range(0.3f, 2.4f);
                int id = Random.Range(0, _tutorialBaseFieldView.CurrentView.ColumVews.Length - 1);

                ColumVew columVew = _tutorialBaseFieldView.CurrentView.ColumVews[id];
                DOVirtual.DelayedCall(delay, () =>
                {
                    int part = Random.Range(1, 6)/2;
                    int angle = part*360;
                    columVew.transform.DORotate(new Vector3(angle, 0, 0), part, RotateMode.LocalAxisAdd)
                        .OnComplete(() => RotateColum());
                });
            }
        }
    }
}