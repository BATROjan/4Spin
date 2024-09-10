using Zenject;

namespace UI.UISettingsWindow
{
    public class UISettingsInstaller : Installer<UISettingsInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<UISettingsWindowController>()
                .AsSingle()
                .NonLazy();
        }
    }
}