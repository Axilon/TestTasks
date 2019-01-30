using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController instance;
    [SerializeField]
    private PrefabsData prefabsData;
    public PrefabsData SetPrefabsData
    {
        set
        {
            prefabsData = value;
            Debug.Log("prefabsData setted ");
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void CreateRandomMesh(Vector3 rayHitPosition)
    {
        int randomPrefabNumber = Random.Range(0, prefabsData.prefabsNames.Length);
        string prefabType = prefabsData.prefabsNames[randomPrefabNumber];
        GameObject prefab = PrefabsController.instance.GetPrefab(prefabType);
        if(prefab == null) {
            CreateRandomMesh(rayHitPosition);
            return;
        }
        GameObject mesh = Instantiate(prefab);
        mesh.name = prefabType;
        rayHitPosition.y = mesh.transform.position.y;
        mesh.transform.position = rayHitPosition;
    }
}
