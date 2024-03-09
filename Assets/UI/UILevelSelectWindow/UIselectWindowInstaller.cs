using Zenject;

namespace UI.UILevelSelectWindow
{
    public class UIselectWindowInstaller : Installer<UIselectWindowInstaller>
    {
        public override void InstallBindings()
        {  
            Container
            .Bind<UIlevelSelectWindowController>()
            .AsSingle()
            .NonLazy();
        }
    }
}