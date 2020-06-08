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

    public void SpawnEnemy(List<GameObject> spawners)
    {
        Debug.Log(spawners);

        int spawnPointIndex = Random.Range(0, transform.childCount);

        //Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];

        var randomWaveIndex = Random.Range(0, waves.Length);

        Debug.Log(waves[randomWaveIndex].enemy.Length);

        for (int i = 0; i < waves[randomWaveIndex].enemy.Length; i++)
        {
            //Instantiate(waves[randomWaveIndex].enemy[i], spawners[Random.Range(0, spawners.Count)].transform.position, transform.rotation);
            Instantiate(waves[randomWaveIndex].enemy[i], spawners[i].transform.position, transform.rotation);
        }
    }
}