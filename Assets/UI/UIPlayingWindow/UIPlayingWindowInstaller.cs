using Zenject;

namespace UI.UIPlayingWindow
{
    public class UIPlayingWindowInstaller : Installer<UIPlayingWindowInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<UIPlayingWindowController>()
                .AsSingle()
                .NonLazy();
        }
    }
}