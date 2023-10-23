using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Reference to enemy prefab
    public GameObject enemyPrefab;
    // Reference to player transform
    public Transform playerTransform;

    // List of spawn points
    public List<Transform> spawnPoints = new List<Transform>();

    [SerializeField] private List<GameObject> enemyList = new List<GameObject>();

    // Reference to spawn zone
    [SerializeField] private float spawnZoneRadius = 15f;
    private float spawnZoneRadiusSqr;
    // Spawn amount
    public int spawnAmount = 3;

    private Dictionary<Transform, GameObject> spawnPointEnemyLink = new Dictionary<Transform, GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        spawnZoneRadiusSqr = spawnZoneRadius * spawnZoneRadius;
        
        //List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);
        
        //// Get the spawn points
        //for (int i = 0; i < spawnAmount; i++)
        //{
        //    if (availableSpawnPoints.Count <= 0)
        //    {
        //        Debug.Log("Not enough spawn points.");
        //        return;
        //    }

        //    int index = Random.Range(0, availableSpawnPoints.Count);
        //    Transform spawnPos = availableSpawnPoints[index];
            
        //    // Spawn enemy at random available spawn point
        //    GameObject enemy = Instantiate(enemyPrefab, spawnPos.position, Quaternion.Euler(0f, 0f, 0f));
        //    enemyList.Add(enemy);
        //    enemy.SetActive(false);
            
        //    // Remove spawn point from available spawn points
        //    availableSpawnPoints.RemoveAt(index);
        //}

        foreach (Transform spawnPoint in spawnPoints)
        {
            // Spawn enemy at random available spawn point
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.Euler(0f, 0f, 0f));
            spawnPointEnemyLink.Add(spawnPoint, enemy);
            enemy.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if ((playerTransform.position - transform.position).sqrMagnitude < spawnZoneRadiusSqr)
        //{
        //    for (int i = 0; i < enemyList.Count; i++)
        //    {
        //        enemyList[i].SetActive(true);
        //    }
        //}
        //else if ((playerTransform.position - transform.position).sqrMagnitude > spawnZoneRadiusSqr)
        //{
        //    for (int i = 0; i < enemyList.Count; i++)
        //    {
        //        enemyList[i].SetActive(false);
        //    }
        //}

        if ((playerTransform.position - transform.position).sqrMagnitude < spawnZoneRadiusSqr)
        {
            foreach (Transform spawnPoint in spawnPointEnemyLink.Keys)
            {
                GameObject enemy = spawnPointEnemyLink[spawnPoint];
                enemy.SetActive(true);
            }
        }
        else if ((playerTransform.position - transform.position).sqrMagnitude > spawnZoneRadiusSqr)
        {
            foreach (Transform spawnPoint in spawnPointEnemyLink.Keys)
            {
                GameObject enemy = spawnPointEnemyLink[spawnPoint];
                enemy.SetActive(false);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnZoneRadius);
    }
}
