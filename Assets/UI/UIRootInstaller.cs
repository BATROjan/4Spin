using UI.UIStartWindow;
using Zenject;

namespace UI
{
    public class UIRootInstaller : Installer<UIRootInstaller>
    {
        public override void InstallBindings()
        {
            UIStartWindowInstaller
                .Install(Container);
            
            Container
                .InstantiatePrefabResourceForComponent<UIRoot>("UIRoot");
        }
    }
}