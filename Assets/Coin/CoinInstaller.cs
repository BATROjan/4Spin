using Zenject;

namespace Coin
{
    public class CoinInstaller : Installer<CoinInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<CoinConfig>()
                .FromScriptableObjectResource("CoinConfig")
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<CoinController>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindMemoryPool<CoinView, CoinView.Pool>()
                .FromComponentInNewPrefabResource("Coin");
        }
    }
}