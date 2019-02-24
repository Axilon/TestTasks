using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour,  IDamagebl{

    public bool isAlive;

    [SerializeField]
    private int maxHealth;
    private int currentHealth;

    public delegate void IsDead();
    public IsDead isDead;
    // Use this for initialization
    void Start () {
        Initialize();
    }
	
    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            ScoreManager.instance.UpdateHealth(0);
            if (isDead != null)
            {
                isDead();
            }
        }
        else
        {
            ScoreManager.instance.UpdateHealth(currentHealth);
        }
    }

    public void SetValues()
    {
        currentHealth = maxHealth;
        isAlive = true;
        ScoreManager.instance.UpdateHealth(currentHealth);

    }
    public void Destroyed()
    {
        isAlive = false;
        SetValues();
    }

    private void Initialize()
    {
        isDead += Destroyed;
        SetValues();
    }
    
    private void OnApplicationQuit()
    {
        isDead -= Destroyed;
    }
}
