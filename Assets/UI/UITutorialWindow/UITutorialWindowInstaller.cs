using Zenject;

namespace UI.UITutorialWindow
{
    public class UITutorialWindowInstaller : Installer<UITutorialWindowInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<UItutorialWindowController>()
                .AsSingle()
                .NonLazy();
        }
    }
}