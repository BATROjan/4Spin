using Grid;
using Zenject;

namespace PlayingField
{
    public class PLayerFieldInstaller : Installer<PLayerFieldInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<PlayingFieldConfig>()
                .FromScriptableObjectResource("PLayingFieldConfig")
                .AsSingle()
                .NonLazy();  
            
            Container
                .Bind<BackSpriteConfig>()
                .FromScriptableObjectResource("BackSpriteConfig")
                .AsSingle()
                .NonLazy();


            Container
                .Bind<PlayingFieldController>()
                .AsSingle()
                .NonLazy();

            Container
                .BindMemoryPool<PlayingFieldView, PlayingFieldView.Pool>()
                .FromComponentInNewPrefabResource("PlayingFieldView");
            
            Container
                .BindMemoryPool<BackSpriteView, BackSpriteView.Pool>()
                .FromComponentInNewPrefabResource("BackSpriteView");
        }
    }
}