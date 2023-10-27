using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnZone : MonoBehaviour
{
    public List<Transform> spawnPoints = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        SetUpSpawnPoints();
    }

    private void SetUpSpawnPoints()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            EnemySpawnPoint enemySpawnPoint = spawnPoint.GetComponent<EnemySpawnPoint>();
            enemySpawnPoint.SpawnEnemy();
        }
    }

    private void EnableEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            EnemySpawnPoint enemySpawnPoint = spawnPoint.GetComponent<EnemySpawnPoint>();

            if (!enemySpawnPoint.hasDied)
            {
                enemySpawnPoint.enemy.SetActive(true);
            }
        }
    }

    private void DisableEnemies()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            EnemySpawnPoint enemySpawnPoint = spawnPoint.GetComponent<EnemySpawnPoint>();

            if (enemySpawnPoint.enemy.activeSelf)
            {
                enemySpawnPoint.enemy.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnableEnemies();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DisableEnemies();
        }
    }
}
