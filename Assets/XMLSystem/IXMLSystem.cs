using System.Collections.Generic;

namespace XMLSystem
{
    public interface IXMLSystem
    {
        void CreatXMLFile();
        public void SaveSoundValueToXML(AudioGroupType type, string value);
        string LoadFromXML(string key, string value);
    }
}