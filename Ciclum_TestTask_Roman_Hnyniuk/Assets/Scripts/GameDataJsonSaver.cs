using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DataSaver
{
    public class GameDataJsonSaver : GameDataSaver
    {
         public override void SaveString(string key, string value)
            {

            }
            public override void SaveBool(string key, bool value)
            {

            }
            public override void SaveInt(string key, int value)
            {

            }
            public override void SaveFloat(string key, float value)
            {

            }


            public override string LoadString(string key)
            {
                return "";
            }
            public override bool LoadBool(string key)
            {
                return false;
            }

            public override int LoadInt(string key)
            {
                return 0;
            }
            public override float LoadFloat(string key)
            {
                return 0;
            }

            public override void SaveData()
            {
                throw new System.NotImplementedException();
            }
    }
}
