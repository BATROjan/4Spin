using UnityEngine;

namespace Tutorial
{
    public class TutorialFieldView : MonoBehaviour
    {
        public ColumVew[] ColumVews => columVews;

        [SerializeField] private ColumVew[] columVews;
    }
}