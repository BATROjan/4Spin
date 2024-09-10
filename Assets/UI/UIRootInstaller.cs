using UI.UILevelSelectWindow;
using UI.UIPauseGameWindow;
using UI.UIPlayingWindow;
using UI.UISelectOpponetWindow;
using UI.UIService;
using UI.UISettingsWindow;
using UI.UIStartWindow;
using UI.UITutorialWindow;
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
            
            UISelectOpponentWindowInstaller
                .Install(Container);
            
            UIWinWindowInstaller
                .Install(Container);
            
            UITutorialWindowInstaller
                .Install(Container);

            UISettingsInstaller
                .Install(Container);
            
            UIPauseGameInstaller
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