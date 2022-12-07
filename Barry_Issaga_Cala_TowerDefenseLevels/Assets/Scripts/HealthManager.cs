using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HealthManager : MonoBehaviour
{
    public enum EnemyType { Enemy01, Enemy02}
    [Header("Health settings")]
    [SerializeField] private EnemyType currentEnemyType;
    [SerializeField] private Image hpBar;

    [Header("Stats")]
    [SerializeField] private int maxHealth;
    int currentHealth;

    public static Action<EnemyController>onEnemyDead;
    EnemyController enemy;

    public static Action<EnemyController>onEnemyHurt;

    private bool enemyDead;

    void Start()
    {
        currentHealth = maxHealth;
        HpBarUpdate();
        enemy = GetComponent<EnemyController>();
    }


    public void TakeDamage(int damage)
    {
        if (enemyDead)
            return;


        currentHealth -= damage;

        HpBarUpdate();

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            if(currentEnemyType == EnemyType.Enemy01)
            {
                //die
                EnemyDead();
            }

            if (currentEnemyType == EnemyType.Enemy02)
            {
                //die
                EnemyDead();
            }
        }
        else
        {
            onEnemyHurt?.Invoke(enemy);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(10);
        }
    }

    public void RestartHealth()
    {
        currentHealth = maxHealth;

        HpBarUpdate();

        Invoke("ResetEnemyDeadLater", 0.1f);
    }

    public void HpBarUpdate()
    {
        hpBar.fillAmount = (float)currentHealth / maxHealth;
    }

    void EnemyDead()
    {
        enemyDead = true;
        enemy.ResetCurrentPathPoint();

       // RestartHealth();

        onEnemyDead?.Invoke(enemy);
        //gameObject.SetActive(false);
    }

    public bool GetEnemyisDead()
    {
        return enemyDead;
    }

    private void ResetEnemyDeadLater()
    {
        enemyDead = false;
        enemy.ResumeMovement();
    }
        
}
