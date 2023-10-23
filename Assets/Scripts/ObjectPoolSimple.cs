using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolSimple : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<GameObject> enemyPool = new List<GameObject>();
    [SerializeField] private int poolStartSize = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolStartSize; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemyPool.Add(enemy);
            enemy.SetActive(false);
        }
    }

    public GameObject GetEnemy()
    {
        if (enemyPool.Count > 0)
        {
            GameObject enemy = enemyPool[0];
            enemyPool.RemoveAt(0);
            enemy.SetActive(true);
            return enemy;
        }
        else
        {
            GameObject enemy = Instantiate(enemyPrefab);
            return enemy;
        }
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemyPool.Add(enemy);
        enemy.SetActive(false);
    }
}
