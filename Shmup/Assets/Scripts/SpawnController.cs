using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public GameObject EnemyPrefab;
    //public List<Transform> spawnPoints;
    public Vector3 spawnPosition;
    public int enimiesToSpawn;

    private void Start()
    {
        for (int i = 0; i < enimiesToSpawn; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(-spawnPosition.x, spawnPosition.x), spawnPosition.y, spawnPosition.z);

            SpawnEnemy(randomPosition);
        }
    }

    public void SpawnEnemy(Vector3 enemyPosition)
    {
        Instantiate(EnemyPrefab, enemyPosition, Quaternion.identity);
    }
}
