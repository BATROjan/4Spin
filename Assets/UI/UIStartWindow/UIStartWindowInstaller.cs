using System.Security.Cryptography;
using Zenject;

namespace UI.UIStartWindow
{
    public class UIStartWindowInstaller : Installer<UIStartWindowInstaller>
    {
        public override void InstallBindings()
        {
            /*Container
                .InstantiatePrefabResourceForComponent<UIStartWindow>("UIStartWindow");*/
        }
    }
}