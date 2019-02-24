using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    [SerializeField]
    private int maxEnemyAmmount;
    private int currentEnemyAmmount;
    [SerializeField]
    private float spawningTime_Min;
    [SerializeField]
    private float spawningTime_Max;
    [SerializeField]
    private float spawningDistance;

    [SerializeField]
    private List<GameObject> avaliableEnemiesPrefabs;
    private Queue<GameObject> enemiesPool;

    public delegate void GameEnded();
    public static event GameEnded OnGameEnded;

    public static EnemyManager instance;
    
    void Awake () {
        InitializePool();
    }

    public void AddEnemyToPool(GameObject enemy)
    {
        enemiesPool.Enqueue(enemy);
        enemy.SetActive(false);
    }

    public void AddAllEnemiesToPool()
    {
        if (OnGameEnded != null)
        {
            OnGameEnded();
            StopCoroutine("SpawnEnemies");
        }
    }
    public void StartSpawningEnemies()
    {
        StartCoroutine("SpawnEnemies");
    }
    private void InitializePool()
    {
        if (instance == null)
        {
            instance = this;
        }
        enemiesPool = new Queue<GameObject>();
        for (int i = 0; i < 15; i++)
        {
            CreateNewEnemyAndAddToPool();
        }
    }
    private void RemoveEnemyFromPool()
    {
        if (currentEnemyAmmount < maxEnemyAmmount)
            {
                if (enemiesPool.Count == 0)
                {
                    CreateNewEnemyAndAddToPool();
                }
                GameObject enemy = enemiesPool.Dequeue();
                float baseY = enemy.transform.position.y;
                enemy.transform.position = MathCalculations.CalculateNewPositionByRadiusAndAngle(spawningDistance, Random.Range(0f, 359f), baseY);
                enemy.SetActive(true);
        }
    }
    private void CreateNewEnemyAndAddToPool()
    {
        
            AddEnemyToPool(Instantiate(avaliableEnemiesPrefabs[Random.Range(0, avaliableEnemiesPrefabs.Count)], transform));
            currentEnemyAmmount++;
        
    }

    IEnumerator SpawnEnemies()
    {
        while (!GameController.instance.GameFinished)
        {
            yield return new WaitForSeconds(Random.Range(spawningTime_Min, spawningTime_Max));
            RemoveEnemyFromPool();
        }
    }
}
