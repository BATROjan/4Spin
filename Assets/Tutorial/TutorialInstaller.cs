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
        }
    }
}