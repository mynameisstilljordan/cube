using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject caution;
    private int enemyChoice;
    float xSpawn, ySpawn;
    float spawnXChoice;
    // Start is called before the first frame update
    void Start()
    {
        ySpawn = Random.Range(-4.5f, 4.5f);
        spawnXChoice = Random.Range(0,1); if (spawnXChoice <= 0.5f) xSpawn = -2.5f; else xSpawn = 2.5f;
        // if (GameHandlerScript.score <= 10) enemyChoice = Random.Range()
    }

    public void SpawnEnemy()
    {
        Instantiate(caution, new Vector2(xSpawn, ySpawn), Quaternion.identity);
        ySpawn = Random.Range(-4.5f, 4.5f);
        spawnXChoice = Random.Range(0, 1); if (spawnXChoice <= 0.5f) xSpawn = -2.5f; else xSpawn = 2.5f;
    }
}
