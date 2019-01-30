using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GameData")]
public class GameData : ScriptableObject{
    public int ObservableTime;

    private static GameData gameData;
    public static GameData GetGameData
    {
        get {
            if (gameData == null)
            {
                gameData = Resources.Load<GameData>("GameData");
            }
            return gameData;
        }
    }
    
}
