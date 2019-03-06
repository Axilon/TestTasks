using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace DataSaver
{
    public class GameDataFileSaver : GameDataSaver
    {
        private string folderPath= Application.streamingAssetsPath + "/" ;
        private string fileName = "Data.db";
        private char separetor = '|';
        private Dictionary<string, object> gameData;
        public GameDataFileSaver()
        {
            gameData = new Dictionary<string, object>();
            if (!File.Exists(folderPath+fileName))
            {
                FileStream fs = File.Create(folderPath + fileName);
                fs.Close();
            }
            LoadData();
        }
        
        #region Save Values Region
        public override void SaveString(string key, string value)
        {
            PutValueToDictionary(key, value);
        }
        public override void SaveBool(string key, bool value)
        {
            PutValueToDictionary(key, value);
        }
        public override void SaveInt(string key, int value)
        {
            PutValueToDictionary(key, value);
        }
        public override void SaveFloat(string key, float value)
        {
            PutValueToDictionary(key, value);
        }
        #endregion
        
        #region Load Values Region
        public override string LoadString(string key)
        {
            try
            {
                return (string)GetValueFromDictionary(key);
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException();
            }
        }
        public override bool LoadBool(string key)
        {
            try
            {
                return (bool)GetValueFromDictionary(key);
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException();
            }

        }
        public override int LoadInt(string key)
        {
            try
            {
                return (int)GetValueFromDictionary(key);
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException();
            }
        }
        public override float LoadFloat(string key)
        {
            try
            {
                return (float)GetValueFromDictionary(key);
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException();
            }
        }
        #endregion

        public override void SaveData()
        {
            SaveToFile();
        }
        private void PutValueToDictionary(string key, object value)
        {
            if (gameData.ContainsKey(key))
            {
                gameData[key] = value;
            }
            else
            {
                gameData.Add(key, value);
            }
        }
        private object GetValueFromDictionary(string key)
        {
            if (gameData.ContainsKey(key))
            {
                return gameData[key];
            }
            else
            {
                throw new NullKeyValueException(key);
            }
        }
        private void WriteToFile(string type, string key, string value)
        {
            using (StreamWriter fileWriter = new StreamWriter(folderPath + fileName, true))
            {
                fileWriter.WriteLine(Coder.Encode(type + separetor + key+ separetor + value));
            }
        }
        private void LoadData()
        {
            string line = "";
            using (StreamReader fileReader = new StreamReader(folderPath + fileName))
            {
                while ((line = fileReader.ReadLine()) != null)
                {
                    AddToDictionary(Coder.Decode(line));
                }
            }
        }
        
        private void SaveToFile()
        {
            File.WriteAllText(folderPath + fileName, "");
            string type = "";
            string key = "";
            string value = "";
            foreach (KeyValuePair<string, object> keyValue in gameData)
            {
                key = keyValue.Key;
                value = keyValue.Value.ToString();
                type = keyValue.Value.GetType().Name;
                WriteToFile(type, key, value);
            }
        }
        private void AddToDictionary(string lineText)
        {
            Debug.Log(lineText);
            string[] splitedText = lineText.Split(separetor);
            string type = splitedText[0];
            string key = splitedText[1];

            switch (type)
            {
                case ("String"):
                    gameData.Add(key, splitedText[2]);
                    break;
                case ("Boolean"):
                    gameData.Add(key, bool.Parse(splitedText[2]));
                    break;
                case ("Int32"):
                    gameData.Add(key, int.Parse(splitedText[2]));
                    break;
                case ("Single"):
                    gameData.Add(key, float.Parse(splitedText[2]));
                    break;
            }
        }
        
    }
}
