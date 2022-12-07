using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretUpgradeController : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private int updateInitialCost;
    [SerializeField] private int updateCostIncremental;
    [SerializeField] private int damageIncremental;

    private TurretProjectileController turretProjectile;

    [SerializeField] private float delayReduce;

    public int upgradeCost { get; set; }
    private CurrencyController currencyController;

    
    void Start()
    {
        turretProjectile = GetComponent<TurretProjectileController>();
        upgradeCost = updateInitialCost;
        currencyController = GameObject.FindGameObjectWithTag("Currency").GetComponent<CurrencyController>();
    }

    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {
            UpgradeTurret();
        }
    }

    public void UpgradeTurret()
    {
        //Si no hay dinero no puede hacer lo de abajo
        if (currencyController.currentCurrency < upgradeCost)
            return;

        //Mejora del daño
        turretProjectile.damageToAssign += damageIncremental;

        //Reducir el tiempo
        turretProjectile.delayPershot -= delayReduce;

        if (turretProjectile.delayPershot < 0)
            turretProjectile.delayPershot = 0;

        //Quitar las monedas
        UpdateTurretUpgrade();
    }

    public void UpdateTurretUpgrade()
    {
        currencyController.RemoveCurrency(upgradeCost);
        upgradeCost += updateCostIncremental;
    }
}
