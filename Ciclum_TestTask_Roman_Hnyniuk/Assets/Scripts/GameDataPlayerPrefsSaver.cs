using UnityEngine;
using System;
namespace DataSaver
{
    public class GameDataPlayerPrefsSaver : GameDataSaver, IDataSavebl
    {
        #region Save Values Region
        public override void SaveString(string key, string value)
        {
            SaveEncodedValue(key, value);
        }
        public override void SaveBool(string key, bool value)
        {
            SaveEncodedValue(key, value.ToString());
        }
        public override void SaveInt(string key, int value)
        {
            SaveEncodedValue(key, value.ToString());
        }
        public override void SaveFloat(string key, float value)
        {
            SaveEncodedValue(key, value.ToString());
        }
        #endregion

        #region Load Values Region
        public override string LoadString(string key)
        {
            return GetDecodedValue(key);
        }
        public override bool LoadBool(string key)
        {
              if(!bool.TryParse(GetDecodedValue(key), out bool result))
               {
                   throw new InvalidCastException();
               }
               return result;
        }
        public override int LoadInt(string key)
        {
            if (!int.TryParse(GetDecodedValue(key), out int result))
            {
                throw new InvalidCastException();
            }
            return result;
        }
        public override float LoadFloat(string key)
        {
            if (!float.TryParse(GetDecodedValue(key), out float result))
            {
                throw new InvalidCastException();
            }
            return result;
        }
        #endregion
        public override void SaveData()
        {
            PlayerPrefs.Save();
        }
        private void SaveEncodedValue(string key, string value)
        {
            PlayerPrefs.SetString(Coder.Encode(key), Coder.Encode(value));
        }

        private string GetDecodedValue(string key)
        {
            if (!PlayerPrefs.HasKey(Coder.Encode(key)))
            {
                throw new NullKeyValueException(key);
            }
            string value = Coder.Decode(PlayerPrefs.GetString(Coder.Encode(key)));
            return value;
        }
        
    }
}
