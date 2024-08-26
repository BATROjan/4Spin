using Zenject;

namespace Environment
{
    public class EnviromentInstaller : Installer<EnviromentInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<EnvironmentController>().AsSingle().NonLazy();
        }
    }
}