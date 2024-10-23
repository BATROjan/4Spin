using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;

namespace XMLSystem
{
    public class XMLSystem : IXMLSystem
    {
        private const string PrefsKey = "User";
        
        private readonly XMLConfig _xmlConfig;
        private readonly string _savePath;

        private const string SaveNodeName = "save_file";

        public XMLSystem(XMLConfig xmlConfig)
        {
            _xmlConfig = xmlConfig;
            _savePath = GenerateSavePath();
        }

        public void CreatXMLFile()
        {
            var xmlDoc = new XmlDocument();
            var rootNode = xmlDoc.CreateElement(SaveNodeName);
            xmlDoc.AppendChild(rootNode);
            xmlDoc.Save(_savePath + _xmlConfig.SaveName);
        }

        public string LoadFromXML(string key, string value)
        {
            var loadPath = _savePath + _xmlConfig.SaveName;
            if (!File.Exists(loadPath))
            {
                SaveAll();
            }

            var xmlDoc = new XmlDocument();
            xmlDoc.Load(loadPath);

            var node = xmlDoc.SelectSingleNode($"/{SaveNodeName}/{key}");
            if (node == null)
            {
                SaveSoundValueToXML(ParseEnum<AudioGroupType>(key), "1");

                xmlDoc.Load(loadPath);
                node = xmlDoc.SelectSingleNode($"/{SaveNodeName}/{key}");
            }
            
            return node.Attributes[value].Value;
        }

        private void SaveAll()
        {
            SaveSoundValueToXML(AudioGroupType.Main, "1");
            SaveSoundValueToXML(AudioGroupType.Effect, "1");
        }
        
        private T ParseEnum<T>(string value)
        {
            return (T) AudioGroupType.Parse(typeof(T), value, true);
        }

        public void SaveSoundValueToXML(AudioGroupType type, string value)
        {
             var xmlDoc = new XmlDocument();
             var rootNode = xmlDoc.DocumentElement;
            var loadPath = _savePath + _xmlConfig.SaveName;
            if (File.Exists(loadPath))
            {
                xmlDoc.Load(_savePath + _xmlConfig.SaveName);
                rootNode = xmlDoc.DocumentElement;
            }
            else
            {
                rootNode = xmlDoc.CreateElement(SaveNodeName);
                xmlDoc.AppendChild(rootNode);
            }
            
            var node = xmlDoc.SelectSingleNode($"/{SaveNodeName}/{type}");
            if (node != null)
            {
                node.Attributes["value"].Value = value;
            }
            else
            {
                var elem = xmlDoc.CreateElement(type.ToString());
                elem.SetAttribute("value", value);
                rootNode.AppendChild(elem);
            }
            
            xmlDoc.Save(_savePath + _xmlConfig.SaveName);
        }

        private string GenerateSavePath()
        {
            var computerPath = Application.dataPath + _xmlConfig.SourcePath;
            var phonePath = Application.persistentDataPath;
        
#if UNITY_ANDROID
        if (!Directory.Exists(phonePath))
        {
            Directory.CreateDirectory(phonePath);
        }
        return phonePath;
#elif UNITY_IPHONE
        phonePath += "/";
        if (!Directory.Exists(phonePath))
        {
            Directory.CreateDirectory(phonePath);
        }
        return phonePath;
#elif UNITY_EDITOR
            computerPath = Application.dataPath;
            computerPath = computerPath.Remove(computerPath.IndexOf("/Assets"), 7);
            computerPath += _xmlConfig.SourcePath;
            if (!Directory.Exists(computerPath))
            {
                Directory.CreateDirectory(computerPath);
            }
            return computerPath;
#else
            if (!Directory.Exists(computerPath))
            {
                Directory.CreateDirectory(computerPath);
            }
            return computerPath;
#endif
        }
    }
}