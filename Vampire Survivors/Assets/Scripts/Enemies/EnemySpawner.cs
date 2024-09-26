using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private Transform Player;

    private int waveLevel = -1;
    private int placeInWave;
    private int SpawnLimit = 150;
    private float timeBetweenSpawns;
    private float timeBetweenWave;
    private bool spawning;
    private WaveComponent currentWave;

    [System.Serializable]
    public class WaveComponent
    {
        public EnemyObject[] enemyTypes;
        public int[] SpawnsPerEnemy;
        public int[] spawned;
        public float SpawnTime;
        public float WaveTime;
        public int SpawnMultiplier;
    }

    public WaveComponent[] waveComps;
    public GameObject[] Spawners;

    private void Start()
    {
        StartSpawning();
    }

    private void Update()
    {
        if (spawning)
        {
            timeBetweenSpawns -= Time.deltaTime;
            if(timeBetweenSpawns <= 0)
            {
                    timeBetweenSpawns = currentWave.SpawnTime;
                    for (int i = currentWave.spawned[placeInWave]; i < currentWave.SpawnsPerEnemy[placeInWave];)
                    {
                        currentWave.spawned[placeInWave]++;
                        if (GameManager.TotalEnemies <= SpawnLimit)
                        {
                            SpawnEnemy(currentWave.enemyTypes[placeInWave]);
                            GameManager.TotalEnemies++;
                        }
                        break;
                    }

                    if(currentWave.spawned[placeInWave] >= currentWave.SpawnsPerEnemy[placeInWave])
                    {
                        placeInWave++;
                        if(placeInWave >= currentWave.enemyTypes.Length)
                        {
                            spawning = false;
                        }
                    }
            }
        }
        else
        {
            timeBetweenWave -= Time.deltaTime;
            if(timeBetweenWave <= 0)
            {
                StartSpawning();
            }
        }
    }


    public void StartSpawning() {
        waveLevel++;
        GameManager.WaveLevel++;
        if(GameManager.WaveLevel > 25){
            SpawnLimit += 50;
        }
        if (waveLevel >= waveComps.Length)
        {
            waveLevel = 0;
            foreach (WaveComponent wc in waveComps)
            {
                for(int i = 0 ; i < wc.SpawnsPerEnemy.Length ; i++)
                {
                    wc.SpawnsPerEnemy[i] *= wc.SpawnMultiplier;
                }
            }
        }
        currentWave = waveComps[waveLevel];
        placeInWave = 0;
        spawning = true;
        timeBetweenWave = currentWave.WaveTime;
        int x = 0;
        foreach (int num in currentWave.spawned)
        {
            currentWave.spawned[x] = 0;
            x++;
        }
    }

    private void SpawnEnemy(EnemyObject type){
        Instantiate(Enemy, Spawners[Random.Range(0,Spawners.Length -1)].transform.position, Quaternion.identity).GetComponent<EnemyBehaviour>().Spawn(type, Player, waveComps.Length);
    }

}
