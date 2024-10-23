using UnityEngine;

namespace XMLSystem
{
    [CreateAssetMenu(fileName = "XMLConfig", menuName = "Configs/XMLConfig")]

    public class XMLConfig : ScriptableObject
    {
        public string SourcePath;
        public string SaveName;
    }
}