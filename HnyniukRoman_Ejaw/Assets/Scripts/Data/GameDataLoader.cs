using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataLoader : MonoBehaviour {
    private string gameDataFileName = "data.json";
    
	// Use this for initialization
	void Start () {
        LoadGameData();
    }
	
    private void LoadGameData()
    {
        string jsonFilePath = gameDataFileName.Replace(".json", "");
        TextAsset loadedJsonFile = Resources.Load<TextAsset>(jsonFilePath);
        GameController.instance.SetPrefabsData = JsonUtility.FromJson<PrefabsData>(loadedJsonFile.text);
    }
}
