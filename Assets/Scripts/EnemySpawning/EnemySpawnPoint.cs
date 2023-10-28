using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    public GameObject enemyPrefab;
    [HideInInspector] public GameObject enemy;

    public bool hasDied = false;
    
    public void SpawnEnemy()
    {
        hasDied = false;

        enemy = Instantiate(enemyPrefab, transform.position, Quaternion.Euler(0f, 0f, 0f), transform);
        enemy.SetActive(false);
    }

    public void EnemyDied()
    {
        hasDied = true;
    }

    public void SpawnPointReset()
    {
        hasDied = false;
    }
}
