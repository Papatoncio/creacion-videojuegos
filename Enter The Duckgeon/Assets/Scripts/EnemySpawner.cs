using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemy;
    public GameObject spawnPoint;
    public float spawnDelay;
    public float passedTime;

    private int size;

    // Start is called before the first frame update
    void Start()
    {
        size = enemy.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (passedTime >= spawnDelay)
        {
            SpawnEnemy();
            passedTime = 0;
        }

        if (passedTime < spawnDelay)
        {
            passedTime += Time.deltaTime;
        }
    }

    void SpawnEnemy()
    {
        int index = Random.Range(0, size);
        GameObject enemyClone = Instantiate(enemy[index]);
        enemyClone.transform.position = spawnPoint.transform.position;
    }
}
