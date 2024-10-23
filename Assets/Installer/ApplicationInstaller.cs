using AudioController;
using Coin;
using DragController;
using Environment;
using GameController;
using Grid;
using PlayingField;
using Tutorial;
using UI;
using XMLSystem;
using Zenject;

namespace Installer
{
    public class ApplicationInstaller : MonoInstaller<ApplicationInstaller>
    {
        public override void InstallBindings()
        {
            XMLSystemInstaller
                .Install(Container);
            
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
            
            AudioInstaller
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