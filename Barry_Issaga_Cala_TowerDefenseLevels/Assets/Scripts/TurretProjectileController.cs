using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectileController : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField] protected Transform projectileFirePoint;

    [SerializeField] protected float attackDelay = 0.1f;
    protected float _nextAttackTimer;

    [Header("Pooler settings")]
    [SerializeField] protected int projectilesToStore = 10;
    [SerializeField] protected GameObject projectile;

    protected ObjectPooler pooler;

    [SerializeField] int initialDamage;

    public int damageToAssign { get; set; }

    public float delayPershot { get; set; }

    ProjectileController currentProjectileLoaded;
    protected TurretController turret;

    void Start()
    {
        pooler = new ObjectPooler();
        pooler.StorePoolObject(projectilesToStore, projectile);

        turret = GetComponent<TurretController>();
        damageToAssign = initialDamage;

        delayPershot = attackDelay;
    }

    
   protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadProjectile();
        }

        if (isTurretEmpty())
        {
            LoadProjectile();
        }

        if(Time.time > _nextAttackTimer)
        {
            if (turret.currentEnemyTarget != null && currentProjectileLoaded != null)
            {
                currentProjectileLoaded.transform.parent = null;
                currentProjectileLoaded.SetEnemy(turret.currentEnemyTarget);

            }
            _nextAttackTimer = Time.time + delayPershot;
        }

        
    }

    private void LoadProjectile()
    {
        GameObject _projectile = pooler.GetPoolObject(projectile);
        _projectile.transform.position = projectileFirePoint.position;
        _projectile.transform.SetParent(projectileFirePoint,projectileFirePoint);

        currentProjectileLoaded = _projectile.GetComponent<ProjectileController>();
        currentProjectileLoaded.turretOwner = this;

        currentProjectileLoaded.ResetProjectile();

        currentProjectileLoaded.damage = damageToAssign;

        _projectile.SetActive(true);
    }

    public void ResetTurretProjectile()
    {
        currentProjectileLoaded = null;
    }

    private bool isTurretEmpty()
    {
        return currentProjectileLoaded == null;
    }
}
