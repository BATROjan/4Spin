using Coin;
using DragController;
using Grid;
using PlayingField;
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
            
            CoinInstaller
                .Install(Container);
            
            DragControllerInstaller
                .Install(Container);
            
            Container
                .Bind<GameController.GameController>()
                .AsSingle()
                .NonLazy();
        }
    }
}