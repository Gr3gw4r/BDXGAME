using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner Instance;

    public enum SpawnState { SPAWNING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject[] enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    private float searchCountdown = 1f;

    public GameObject TPPoints;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {

    }

    void Update()
    {
        EnemyIsAlive();
    }

    private void EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;

            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                TPPoints.SetActive(true);
            }
            else
            {
                TPPoints.SetActive(false);
            }
        }
    }

    public void SpawnEnemy(Transform[] mySpawnpoints)
    {
        //Debug.Log("Spawning Enemy: " + _enemy.name);

        int spawnPointIndex = Random.Range(0, transform.childCount);

        //Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

        var randomWaveIndex = Random.Range(0, waves.Length);

        for (int i = 0; i < waves[randomWaveIndex].enemy.Length; i++)
        {
            Debug.Log("spawn");
            Instantiate(waves[randomWaveIndex].enemy[i], mySpawnpoints[Random.Range(0, mySpawnpoints.Length)].transform.position, transform.rotation);
        }
    }
}