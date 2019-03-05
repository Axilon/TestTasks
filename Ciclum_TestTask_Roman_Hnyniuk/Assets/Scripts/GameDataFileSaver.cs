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
                File.Create(folderPath + fileName);
            }
            LoadData();
        }
        public override void SaveString(string key, string value)
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
        public override void SaveBool(string key, bool value)
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
        public override void SaveInt(string key, int value)
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
        public override void SaveFloat(string key, float value)
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

        public override string LoadString(string key)
        {
            if (gameData.ContainsKey(key))
            {
                try
                {
                    return (string)gameData[key];
                }
                catch (InvalidCastException)
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new NullKeyValueException(key);
            }
        }
        public override bool LoadBool(string key)
        {
            if (gameData.ContainsKey(key))
            {
                try
                {
                    return (bool)gameData[key];
                }
                catch (InvalidCastException)
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new NullKeyValueException(key);
            }

        }
        public override int LoadInt(string key)
        {
            if (gameData.ContainsKey(key))
            {
                try
                {
                    return (int)gameData[key];
                }
                catch (InvalidCastException)
                {
                    throw new InvalidCastException();
                }
            }
            else
            {
                throw new NullKeyValueException(key);
            }
        }
        public override float LoadFloat(string key)
        {
            if (gameData.ContainsKey(key))
            {
                try
                {
                    return (float)gameData[key];
                }
                catch (InvalidCastException)
                {
                    throw new InvalidCastException();
                }
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
                fileWriter.WriteLine(type + separetor + key+ separetor + value);
            }
        }
        private void LoadData()
        {
            string line = "";
            using (StreamReader fileReader = new StreamReader(folderPath + fileName))
            {
                while ((line = fileReader.ReadLine()) != null)
                {
                    AddToDictionary(line);
                }
            }
        }
        
        public override void SaveData()
        {
            File.WriteAllText(folderPath + fileName, "");
            string type = "";
            string key = "";
            string value = "";
            foreach(KeyValuePair<string, object> keyValue in gameData)
            {
                key = keyValue.Key;
                value = keyValue.Value.ToString();
                type = keyValue.Value.GetType().Name;
                WriteToFile(type,key,value);
            }

        }
        private void AddToDictionary(string lineText)
        {
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
