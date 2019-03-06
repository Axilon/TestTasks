using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataSaver 
{
    enum GameData { NONE, PLAYER_PREFS,FILE, XML }
    public class GameDataManager : MonoBehaviour , IPrimitiveSavebl, IPrimitiveLoadbl
{
        [SerializeField]
        private GameData gameData;
        [SerializeField]
        private GameDataSaver data;

        public static GameDataManager instance;

        //Using this method just for testing
        //Delete after testing
        private void TESTSave()
        {
            
            SaveString("String1", "Some text");
            SaveString("String2", "Some text");
            SaveString("String3", "Some text");
            SaveString("String2", "Changed text");
            Debug.Log(LoadString("String3"));
            SaveBool("bool1", true);
            Debug.Log("bool1" + LoadBool("bool1"));
            SaveBool("bool2", true);
            SaveBool("bool1", false);
            Debug.Log("bool1" + LoadBool("bool1"));
            SaveInt("int1", 100);
            SaveFloat("float", 1002.4f);
            Debug.Log("int1" + LoadInt("int1"));
            SaveInt("int1", 10);
            Debug.Log("float" + LoadFloat("float"));
            Debug.Log("String2 as float" + LoadFloat("String2"));
        }
        //Using this method just for testing
        //Delete after testing
        private void TestRead()
        {
            Debug.Log("String1 = " + LoadString("String1"));
            Debug.Log("String2 = " + LoadString("String2"));
            Debug.Log("String3 = " + LoadString("String3"));
            Debug.Log("bool1" + LoadBool("bool1"));
            Debug.Log("bool11" + LoadBool("bool11"));
            Debug.Log("float as string" + LoadFloat("String2"));
        }

        private void Awake()
        {
            InitializeGameData();
            //TESTSave();
            TestRead();
        }
        #region Save Values Region
        public void SaveString(string key, string value)
        {
            data.SaveString(key, value);
        }
        public  void SaveBool(string key, bool value)
        {
            data.SaveBool(key, value);
        }
        public  void SaveInt(string key, int value)
        {
            data.SaveInt(key, value);
        }
        public  void SaveFloat(string key, float value)
        {
            data.SaveFloat(key, value);
        }
        #endregion

        #region Load Values Region
        public string LoadString(string key)
        {
            try
            {
                return data.LoadString(key);
            }
            catch (InvalidCastException e)
            {
                Debug.LogError(e.Message);
            }
            catch(NullKeyValueException e)
            {
                Debug.LogError(e.Message);
            }
            return "NULL";
        }
        public  bool LoadBool(string key)
        {
            try
            {
                return data.LoadBool(key);
            }
            catch (InvalidCastException e)
            {
                Debug.LogError(e.Message);
            }
            catch (NullKeyValueException e)
            {
                Debug.LogError(e.Message);
            }
           
            return false;
        }

        public  int LoadInt(string key)
        {
            try
            {
                return data.LoadInt(key);
            }
            catch (InvalidCastException e)
            {
                Debug.LogError(e.Message );
            }
            catch (NullKeyValueException e)
            {
                Debug.LogError(e.Message);
            }
            
            return 0;
        }
        public  float LoadFloat(string key)
        {
            try
            {
                return data.LoadFloat(key);
            }
            catch (InvalidCastException e)
            {
                Debug.LogError(e.Message);
            }
            catch (NullKeyValueException e)
            {
                Debug.LogError(e.Message);
            }
            return 0;
        }
        #endregion

        private void InitializeGameData()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(this);
            switch (gameData)
            {
                case (GameData.NONE)://Using Player Prefs by default
                    data = new GameDataPlayerPrefsSaver();
                    break;
                case (GameData.PLAYER_PREFS):
                    data = new GameDataPlayerPrefsSaver();
                    break;
                case (GameData.FILE):
                    data = new GameDataFileSaver();
                    break;
                case (GameData.XML):
                    data = new GameDataXMLSaver();
                    break;
            }
        }
        private void OnApplicationQuit()
        {
            data.SaveData();
        }
    }
}
