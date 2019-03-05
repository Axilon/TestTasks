using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataSaver
{
    enum GameData { NONE, PLAYER_PREFS,FILE, XML, JSON }
    public class GameDataManager : MonoBehaviour
    {
        [SerializeField]
        private GameData gameData;
        [SerializeField]
        private GameDataSaver data;

        private void Awake()
        {
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
                case (GameData.JSON):
                    //data = new GameDataPlayerPrefsSaver();
                    break;
            }

        }
        /*public override void SaveString(string key, string value)
        {
            throw new System.NotImplementedException();
        }
        public override void SaveBool(string key, bool value)
        {
            throw new System.NotImplementedException();
        }
        public override void SaveInt(string key, int value)
        {
            throw new System.NotImplementedException();
        }
        public override void SaveFloat(string key, float value)
        {
            throw new System.NotImplementedException();
        }


        public override string LoadString(string key)
        {
            throw new System.NotImplementedException();
        }
        public override bool LoadBool(string key)
        {
            throw new System.NotImplementedException();
        }

        public override int LoadInt(string key)
        {
            throw new System.NotImplementedException();
        }
        public override float LoadFloat(string key)
        {
            throw new System.NotImplementedException();
        }*/
    }
}
