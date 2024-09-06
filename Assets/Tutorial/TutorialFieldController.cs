using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Tutorial
{
    public class TutorialFieldController
    {
        private readonly TutorialConfig _tutorialConfig;
        private readonly TutorialBaseFieldView.Pool _tutorialFielPool;

        private TutorialBaseFieldView _tutorialBaseFieldView;
        
        public TutorialFieldController(
            TutorialConfig tutorialConfig,
            TutorialBaseFieldView.Pool tutorialFielPool)
        {
            _tutorialConfig = tutorialConfig;
            _tutorialFielPool = tutorialFielPool;
        }

        public void Spawn(DiffcultLevel diffcultLevel)
        {
          _tutorialBaseFieldView = _tutorialFielPool.Spawn(diffcultLevel);
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
                float aaa = Random.Range(0.3f, 2.4f);
                int bbb = Random.Range(0, _tutorialBaseFieldView.CurrentView.ColumVews.Length - 1);

                ColumVew columVew = _tutorialBaseFieldView.CurrentView.ColumVews[bbb];
                DOVirtual.DelayedCall(aaa, () =>
                {
                    columVew.transform.DORotate(new Vector3(360, 0, 0), 3f, RotateMode.LocalAxisAdd)
                        .OnComplete(() => RotateColum());
                });
            }
        }
    }
}