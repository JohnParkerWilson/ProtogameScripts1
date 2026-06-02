using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner")]
    public GameObject enemyPrefab;

    public float spawnInterval = 3f;
    public int maxEnemies = 10;

    private float spawnTimer;
    private int currentEnemies;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

            TrySpawnEnemy();
        }
    }

    private void TrySpawnEnemy()
    {
        if (currentEnemies >= maxEnemies)
            return;

        GameObject enemy = Instantiate(
            enemyPrefab,
            transform.position,
            Quaternion.identity
        );

        Health health =
            enemy.GetComponent<Health>();

        if (health != null)
        {
            health.OnDeath += HandleEnemyDeath;
        }

        currentEnemies++;
    }

    private void HandleEnemyDeath()
    {
        currentEnemies--;
    }
}