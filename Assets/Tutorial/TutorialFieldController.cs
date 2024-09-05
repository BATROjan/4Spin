using Zenject;

namespace Tutorial
{
    public class TutorialFieldController
    {
        private readonly TutorialConfig _tutorialConfig;
        private readonly TutorialFieldView.Pool _tutorialFielPool;

        private TutorialFieldView _tutorialFieldView;
        
        public TutorialFieldController(
            TutorialConfig tutorialConfig,
            TutorialFieldView.Pool tutorialFielPool)
        {
            _tutorialConfig = tutorialConfig;
            _tutorialFielPool = tutorialFielPool;
        }

        public void Spawn(DiffcultLevel diffcultLevel)
        {
          _tutorialFieldView = _tutorialFielPool.Spawn(diffcultLevel);
        }
        public void DestroyField()
        {
            _tutorialFieldView.DestoyView();
            _tutorialFielPool.Despawn(_tutorialFieldView);
        }
    }
}