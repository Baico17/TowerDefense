using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private int currency;

    public int currentCurrency { get; set; }

    void Start()
    {
        LoadCurrency();

    }

    private void LoadCurrency()
    {
        currentCurrency = currency;
    }

    public void AddCurrency(int amount)
    {
        currentCurrency += amount;
    }

    public void RemoveCurrency(int amount)
    {
        if(currentCurrency >= amount)
        {
            currentCurrency -= amount;
        }
    }

    public void AddCurrencyEnemyDead(EnemyController _enemy)
    {
        AddCurrency(10);
    }

    private void OnEnable()
    {
        HealthManager.onEnemyDead += AddCurrencyEnemyDead;
    }

    private void OnDisable()
    {
        HealthManager.onEnemyDead -= AddCurrencyEnemyDead;
    }
}
