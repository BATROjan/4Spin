using DragController.MouseController;
using UnityEngine;
using Zenject;
using SystemInfo = UnityEngine.Device.SystemInfo;

namespace DragController
{
    public class DragControllerInstaller : Installer<DragControllerInstaller>

    {
        public override void InstallBindings()
        {
            if (SystemInfo.deviceType == DeviceType.Handheld)
            {
                Container.Bind<IDragController>()
                    .To<TouchController.TouchController>()
                    .AsSingle()
                    .NonLazy();
            }
            else
            {
                Container.Bind<IDragController>()
                    .To<MouseDragController>()
                    .AsSingle()
                    .NonLazy();
            }
        }
    }
}