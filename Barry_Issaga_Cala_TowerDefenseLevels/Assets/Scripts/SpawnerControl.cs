using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControl : MonoBehaviour
{
   public enum SpawnerType { Fixed = 0, Random}
    [Header("General Settings")]
    [SerializeField] private SpawnerType spawnerType = SpawnerType.Fixed;

    [Header("Fixed Time Settings")]
    [SerializeField] private float SpawnDelay = 1f;

    [Header("Random Time Settings")]
    [SerializeField] private float minRandomSpawnDelay = 1f;
    [SerializeField] private float maxRandomSpawnDelay = 4f;

    [Header("Enemy Settings")]
    [SerializeField] private int enemiesToSpawn = 10;
    [SerializeField] private GameObject enemy;

    int enemiesSpawned = 0;

    [Header("Pooler Settings")]
    [SerializeField] private int enemiesToStore = 5;
    ObjectPooler pooler;

    [Header("Pooler Settings")]
    [SerializeField] private int timeBetweenWaves = 5;
    int enemiesRemaining = 0;

    void Start()
    {
        StartCoroutine(startTimer());
        pooler = new ObjectPooler();
        pooler.StorePoolObject(enemiesToStore,enemy);

        enemiesRemaining = enemiesToSpawn;
    }

    float SetRandomDelayTime()
    {
        return UnityEngine.Random.Range(minRandomSpawnDelay, maxRandomSpawnDelay);
    }

    void SpawnEnemy()
    {
        //Instantiate(enemy, transform.position, Quaternion.identity);

        GameObject newEnemy = pooler.GetPoolObject(enemy);
        newEnemy.transform.position = transform.position;
        newEnemy.SetActive(true);
    }

    private IEnumerator startTimer()
    {
        if(spawnerType == SpawnerType.Fixed)
        {
            yield return new WaitForSeconds(SpawnDelay);
        }
        else
        {
            yield return new WaitForSeconds(SetRandomDelayTime());
        }

        if(enemiesToSpawn > 0)
        {
            SpawnEnemy();
            enemiesSpawned++;
            enemiesToSpawn--;
            
            StartCoroutine(startTimer());
        }
    }

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        enemiesToSpawn = enemiesRemaining = enemiesSpawned;
        enemiesSpawned = 0;

        StartCoroutine(startTimer());
    }

    private void EnemyDismiss(EnemyController enemy)
    {
        enemiesRemaining--;

        if(enemiesRemaining <= 0)
        {
            StartCoroutine(NextWave());
        }
    }

    private void OnEnable()
    {
        EnemyController.onPathFinished += EnemyDismiss;
        HealthManager.onEnemyDead += EnemyDismiss;
    }

    private void OnDisable()
    {
        EnemyController.onPathFinished -= EnemyDismiss;
        HealthManager.onEnemyDead -= EnemyDismiss;
    }
}
