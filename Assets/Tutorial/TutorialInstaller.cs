using Zenject;

namespace Tutorial
{
    public class TutorialInstaller : Installer<TutorialInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<TutorialConfig>()
                .FromScriptableObjectResource("TutorialConfig")
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<TutorialFieldController>()
                .AsSingle()
                .NonLazy();

            Container
                .BindMemoryPool<TutorialBaseFieldView, TutorialBaseFieldView.Pool>()
                .FromComponentInNewPrefabResource("PlayinField");   
        }
    }
}