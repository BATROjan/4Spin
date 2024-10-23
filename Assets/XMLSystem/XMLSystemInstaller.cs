using Zenject;

namespace XMLSystem
{
    public class XMLSystemInstaller : Installer<XMLSystemInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<XMLConfig>()
                .FromScriptableObjectResource("XMLConfig")
                .AsSingle();

            Container
                .Bind<IXMLSystem>()
                .To<XMLSystem>()
                .AsSingle();
        }
    }
}