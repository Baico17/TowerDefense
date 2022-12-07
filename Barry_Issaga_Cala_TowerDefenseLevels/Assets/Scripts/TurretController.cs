using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("General settings")]
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private Transform rotationPoint;

    private List<EnemyController>enemyList;

    public EnemyController currentEnemyTarget { get; set; }
    void Start()
    {
        enemyList = new List<EnemyController>();
    }

    
    void Update()
    {
        SetCurrentEnemyTarget();
        RotateTowardsTarget();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        GetComponent<CircleCollider2D>().radius = attackRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyController>())
        {
            enemyList.Add(collision.GetComponent<EnemyController>());
        }
    }

    private void SetCurrentEnemyTarget()
    {
        if (enemyList.Count <= 0)
        {
            currentEnemyTarget = null;
            return;
        }

        currentEnemyTarget = enemyList[0];

        if (currentEnemyTarget.GetComponent<HealthManager>().GetEnemyisDead())
        {
            enemyList.Remove(currentEnemyTarget);
        }
    }

    private void RotateTowardsTarget()
    {
        if (currentEnemyTarget == null)
        {
            return;
        }

        Vector3 targetPos = currentEnemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(rotationPoint.transform.up, targetPos, rotationPoint.transform.forward);

        rotationPoint.transform.Rotate(0, 0, angle);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();
        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
        }
    }

    public Transform GetRotationPoint()
    {
        return rotationPoint;
    }
}
