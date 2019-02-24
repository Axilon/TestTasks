using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour,  IDamagebl
{
    [SerializeField]
    private int currentHealth;
    [SerializeField]
    private bool isAlive;
    [SerializeField]
    private bool playerInDamageZone;

    [SerializeField]
    private EnemySettings enemySettings;

    public void AttackPlayer(GameObject player)
    {
        player.GetComponent<Tower>().GetDamage(enemySettings.damage);
        Destroyed();
    }

    public void Destroyed()
    {
        isAlive = false;
        ReturnToPool();
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroyed();
            ScoreManager.instance.AddScore(enemySettings.maxHp);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }
    private void OnEnable()
    {
        InitializeEnemy();
    }
    private void OnDisable()
    {
        EnemyManager.OnGameEnded -= ReturnToPool;
    }
    private void InitializeEnemy()
    {
        currentHealth = enemySettings.maxHp;
        isAlive = true;
        playerInDamageZone = false;
        GetComponent<Renderer>().material.color = enemySettings.color;
        EnemyManager.OnGameEnded += ReturnToPool;
        Vector3 lookPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        lookPos.y = transform.position.y;
        transform.rotation = Quaternion.LookRotation(transform.position - lookPos);
    }
    
    private void ReturnToPool()
    {
        EnemyManager.instance.AddEnemyToPool(gameObject);
    }
    private void OnApplicationQuit()
    {
        EnemyManager.OnGameEnded -= ReturnToPool;
    }
    private void EnemyMove()
    {
        if(isAlive && GameController.instance.GameStarted && !GameController.instance.GameFinished)
        {
            transform.Translate(-Vector3.forward * enemySettings.speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AttackPlayer(collision.gameObject);
        }
    }
}
