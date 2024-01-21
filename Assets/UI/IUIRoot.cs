using UnityEngine;

namespace UI
{
    public interface IUIRoot
    {
        Canvas Canvas { get; set; }
        Camera Camera { get; set; }
        Transform PoolContainer { get; }
    }
}