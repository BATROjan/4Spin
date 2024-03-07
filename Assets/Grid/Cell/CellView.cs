using UnityEngine;
using Zenject;

namespace Grid.Cell
{
    public class CellView : MonoBehaviour
    {
        public class  Pool : MonoMemoryPool<CellView>
        {
            
        }
    }
}