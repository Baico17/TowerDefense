using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGMProjectileController : TurretProjectileController
{

    [SerializeField] private bool isDualTurret;
    [SerializeField] private float SpreadRange;
   
    protected override void Update()
    {
        if(Time.time > _nextAttackTimer)
        {
            if(turret.currentEnemyTarget != null)
            {
                Vector2 directionToTarget = turret.currentEnemyTarget.transform.position - transform.position;
                FireProjectile(directionToTarget);
            }

            _nextAttackTimer = Time.time + delayPershot;
        }
    }

    private void FireProjectile(Vector2 direction)
    {
        GameObject _projectile = pooler.GetPoolObject(projectile);
        _projectile.transform.position = projectileFirePoint.position;

        _projectile.GetComponent<GunMachineProjectilController>().direction = direction;
        _projectile.GetComponent<GunMachineProjectilController>().damage = damageToAssign;

        if(isDualTurret)
        {
            float randomSpread = Random.Range(-SpreadRange, SpreadRange);

            Quaternion _spreadRotation = Quaternion.Euler(0, 0, randomSpread);

            _projectile.GetComponent<GunMachineProjectilController>().direction = _spreadRotation * direction;
        }

        _projectile.SetActive(true);


    }
}
