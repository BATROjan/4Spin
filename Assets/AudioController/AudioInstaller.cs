using Zenject;

namespace AudioController
{
    public class AudioInstaller : Installer<AudioInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<AudioModelConfig>()
                .FromScriptableObjectResource("AudioModelConfig")
                .AsSingle()
                .NonLazy();
        
            Container
                .BindMemoryPool<AudioView, AudioView.Pool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefabResource("AudioView")
                .UnderTransformGroup("AudioViewGroup");
        
            Container
                .Bind<AudioController>()
                .AsSingle();
        }
    }
}