using System;
using System.IO;
using System.Xml;
using UnityEngine;
namespace DataSaver
{
    public class GameDataXMLSaver : GameDataSaver
    {
        private string folderPath = Application.streamingAssetsPath + "/";
        private string fileName = "Data.xml";
        public GameDataXMLSaver()
        {
            if (!File.Exists(folderPath + fileName))
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode rootNode = xmlDoc.CreateElement("GameData");
                xmlDoc.AppendChild(rootNode);
                xmlDoc.Save(folderPath + fileName);
            }

        }
        #region Save Values Region
        public override void SaveString(string key, string value)
        {
            AddValueToFile(key, value);
        }
        public override void SaveBool(string key, bool value)
        {
            AddValueToFile(key, value.ToString());
        }
        public override void SaveInt(string key, int value)
        {
            AddValueToFile(key, value.ToString());
        }
        public override void SaveFloat(string key, float value)
        {
            AddValueToFile(key, value.ToString());
        }
        #endregion

        #region Load Values Region
        public override string LoadString(string key)
        {
            return GetValueFromFile(key);
        }
        public override bool LoadBool(string key)
        {
            if(!bool.TryParse(GetValueFromFile(key),out bool value))
            {
                throw new InvalidCastException();
            }
            return value;
        }

        public override int LoadInt(string key)
        {
            if (!int.TryParse(GetValueFromFile(key), out int value))
            {
                throw new InvalidCastException();
            }
            return value;
        }
        public override float LoadFloat(string key)
        {
            if (!float.TryParse(GetValueFromFile(key), out float value))
            {
                throw new InvalidCastException();
            }
            return value;
        }
        #endregion

        private string GetValueFromFile(string key)
        {
            string result = string.Empty;
            XmlTextReader xmlReader = new XmlTextReader(folderPath + fileName);
            while (xmlReader.Read())
            {
                if (xmlReader.IsStartElement(key)){
                    result = xmlReader.GetAttribute("value");
                    break;
                }
            }
            if (result == string.Empty)
            {
                throw new NullKeyValueException(key);
            }
            xmlReader.Close();
            return result;
        }
        private void AddValueToFile(string key,string value)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(folderPath + fileName);
            if (xmlDoc.GetElementsByTagName(key).Count > 0)
            {
                xmlDoc.GetElementsByTagName(key)[0].Attributes["value"].Value = value;
            }
            else
            {
                XmlElement element = xmlDoc.CreateElement(key);
                element.SetAttribute("value", value);
                xmlDoc.GetElementsByTagName("GameData")[0].AppendChild(element);
            }
            xmlDoc.Save(folderPath + fileName);
        }
    }
}
