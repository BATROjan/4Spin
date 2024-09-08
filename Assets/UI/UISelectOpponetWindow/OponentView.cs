using UnityEngine;
using UnityEngine.UI;

namespace UI.UISelectOpponetWindow
{
    public class OponentView : MonoBehaviour
    {
        public int ID => id;
        public Image[] Images => images;
        
        [SerializeField] private Image[] images;
        [SerializeField] private int id;
    }
}