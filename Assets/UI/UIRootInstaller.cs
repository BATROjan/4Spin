using UI.UILevelSelectWindow;
using UI.UIPlayingWindow;
using UI.UIService;
using UI.UIStartWindow;
using UI.UIWinWindow;
using Zenject;

namespace UI
{
    public class UIRootInstaller : Installer<UIRootInstaller>
    {
        public override void InstallBindings()
        {
            UIStartWindowInstaller
                .Install(Container);
            
            UIselectWindowInstaller
                .Install(Container);
            
            UIPlayingWindowInstaller
                .Install(Container);
            
            UIWinWindowInstaller
                .Install(Container);
            
            Container.Bind<IUIRoot>()
                .FromComponentInNewPrefabResource("UIRoot")
                .AsSingle()
                .NonLazy();

            Container
                .Bind<IUIService>()
                .To<UIService
                    .UIService>()
                .AsSingle()
                .NonLazy();
        }
    }
}