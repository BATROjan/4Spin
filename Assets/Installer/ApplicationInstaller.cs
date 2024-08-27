using Coin;
using DragController;
using Environment;
using GameController;
using Grid;
using PlayingField;
using Tutorial;
using UI;
using Zenject;

namespace Installer
{
    public class ApplicationInstaller : MonoInstaller<ApplicationInstaller>
    {
        public override void InstallBindings()
        {
            CameraInstaller
                .CameraInstaller
                .Install(Container);
            
            UIRootInstaller
                .Install(Container);
            
            PLayerFieldInstaller
                .Install(Container);
            
            GridInstaller
                .Install(Container);
            
            TutorialInstaller
                .Install(Container);
            
            CoinInstaller
                .Install(Container);
            
            DragControllerInstaller
                .Install(Container);
            
            EnviromentInstaller
                .Install(Container);
            
            Container
                .Bind<GameController.GameController>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<GameConfig>()
                .FromScriptableObjectResource("GameConfig")
                .AsSingle()
                .NonLazy();
        }
    }
}