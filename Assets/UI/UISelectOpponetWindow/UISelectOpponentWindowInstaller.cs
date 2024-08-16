using Zenject;

namespace UI.UISelectOpponetWindow
{
    public class UISelectOpponentWindowInstaller : Installer<UISelectOpponentWindowInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<UISelectOpponentWindowController>()
                .AsSingle()
                .NonLazy();
        }
    }
}