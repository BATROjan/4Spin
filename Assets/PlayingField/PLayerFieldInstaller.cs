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
                .Bind<PlayingFieldController>()
                .AsSingle()
                .NonLazy();

            Container
                .BindMemoryPool<PlayingFieldView, PlayingFieldView.Pool>()
                .FromComponentInNewPrefabResource("PlayingFieldView");
        }
    }
}