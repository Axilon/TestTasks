using System.Collections.Generic;
using UnityEngine;

public class PrefabsController : MonoBehaviour {
    public static PrefabsController instance;
    [SerializeField]
    private List<GameObject> prefabsList;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public GameObject GetPrefab(string objectType)
    {
        foreach(GameObject prefab in prefabsList)
        {
            if (prefab.name.Equals(objectType))
            {
                return prefab;
            }
        }
        return null;
    } 
}
