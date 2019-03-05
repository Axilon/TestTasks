using UnityEngine;
using System;
namespace DataSaver
{
    public class GameDataPlayerPrefsSaver : GameDataSaver
    {
        public override void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }
        public override void SaveBool(string key, bool value)
        {
            PlayerPrefs.SetString(key, value.ToString());
        }
        public override void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }
        public override void SaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }


        public override string LoadString(string key)
        {
            string stringValue = PlayerPrefs.GetString(key);
            if(stringValue == null)
            {
                throw new NullKeyValueException(key);
            }
            return stringValue;
        }
        public override bool LoadBool(string key)
        {
            string stringValue = PlayerPrefs.GetString(key);
            if (stringValue == null)
            {
                throw new NullKeyValueException(key);
            }
            else
            {
                if(bool.TryParse(stringValue,out bool result))
                {
                    throw new InvalidCastException();
                }
                return result;
            }
        }

        public override int LoadInt(string key)
        {
            int result = PlayerPrefs.GetInt(key);
            if(result == 0)
            {
                throw new NullKeyValueException(key);
            }
            return result;
        }
        public override float LoadFloat(string key)
        {
            float result = PlayerPrefs.GetFloat(key);
            if (result == 0)
            {
                throw new NullKeyValueException(key);
            }
            return result;
        }
        public override void SaveData()
        {
            PlayerPrefs.Save();
        }
    }
}
