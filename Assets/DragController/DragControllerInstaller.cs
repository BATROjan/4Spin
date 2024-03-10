using DragController.MouseController;
using Zenject;

namespace DragController
{
    public class DragControllerInstaller : Installer<DragControllerInstaller>

    {
        public override void InstallBindings()
        {
            Container.Bind<IDragController>()
                .To<MouseDragController>()
                .AsSingle()
                .NonLazy();
        }
    }
}