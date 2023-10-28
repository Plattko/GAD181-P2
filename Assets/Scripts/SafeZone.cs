using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public List<GameObject> spawnZones = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject spawnZone in spawnZones)
            {
                // Get reference to EnemySpawnPoint script in each child spawn point
                EnemySpawnZone enemySpawnZone = spawnZone.GetComponent<EnemySpawnZone>();
                
                List<Transform> enemySpawnPoints = enemySpawnZone.spawnPoints;
                
                // Reset each spawn point
                foreach (Transform spawnPoint in enemySpawnPoints)
                {
                    spawnPoint.GetComponent<EnemySpawnPoint>().SpawnPointReset();
                }
            }
        }
    }
}
