using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject bossPrefab;
    public GameObject[] miniEnemyPrefabs;
    public int bossRound;

    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    private float spawnRange = 9;
    public int enemyCount;

    public int powerupCount;
    public int waveNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        var randomPowerup = Random.Range(0, powerupPrefabs.Length - 1);
        Instantiate(powerupPrefabs[randomPowerup], GeneratwSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);

        SpawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {

        enemyCount = FindObjectsOfType<EnemyController>().Length;


        if (enemyCount == 0)
        {
            if (waveNumber % bossRound == 0)
            {
                SpawnBossWave(waveNumber);
            }
            else
            {
                SpawnEnemyWave(waveNumber);
            }

            var randomPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerup], GeneratwSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);

        }



    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        waveNumber++;

        var enemyIndex = Random.Range(0, enemyCount + 1);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefabs[enemyIndex], GeneratwSpawnPosition(), enemyPrefabs[enemyIndex].transform.rotation);

        }
    }

    void SpawnBossWave(int currentRound)
    {
        int miniEnemiesToSpawn;

        if (bossRound != 0)
        {
            miniEnemiesToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemiesToSpawn = 1;
        }
        var boss = Instantiate(bossPrefab, GeneratwSpawnPosition(), bossPrefab.transform.rotation);
        boss.GetComponent<EnemyController>().miniEnemySpawnCount = miniEnemiesToSpawn;
    }

    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], GeneratwSpawnPosition(), miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }
    private Vector3 GeneratwSpawnPosition()
    {
        var spawnPosX = Random.Range(-spawnRange, spawnRange);
        var spawnPosZ = Random.Range(-spawnRange, spawnRange);

        var randomPos = new Vector3(spawnPosX, 0, spawnPosZ);


        return randomPos;
    }



}
