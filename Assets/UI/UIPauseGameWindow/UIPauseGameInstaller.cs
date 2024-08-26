using Zenject;

namespace UI.UIPauseGameWindow
{
    public class UIPauseGameInstaller : Installer<UIPauseGameInstaller>

    {
        public override void InstallBindings()
        {
            Container
                .Bind<UIPauseGameWindowController>()
                .AsSingle()
                .NonLazy();
        }
    }
}