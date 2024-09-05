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
                .BindMemoryPool<TutorialFieldView, TutorialFieldView.Pool>()
                .FromComponentInNewPrefabResource("PlayinField");   
        }
    }
}