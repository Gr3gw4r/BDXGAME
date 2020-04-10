using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum spells { Attack, Defend, TimeSlow}

public class GameManager_Sorciere : MonoBehaviour
{
    public static GameManager_Sorciere Instance;

    public float timer;
    private float timeActual;

    public GameObject shoot;
    public float CD;

    public int shootStock;
    private int shootStockActual;

    private bool isDefending = false;

    public float CDSpawn;
    private float CDSpawnActual = 0;

    public GameObject astral;

    public GameObject soldier;
    public Transform parentSpawn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        shootStockActual = shootStock;
        timeActual = timer;
    }

    // Update is called once per frame
    void Update()
    {
        timeActual -= Time.deltaTime;
        CDSpawnActual += Time.deltaTime;

        if (CDSpawnActual >= CDSpawn)
        {
            SpawnEnnemies(soldier);
            CDSpawnActual = 0;
        }

        Debug.Log(timeActual);
    }

    private void SpawnEnnemies(GameObject newEnnemy)
    {
        GameObject EnnemySpawned = Instantiate(newEnnemy, parentSpawn.GetChild(Random.Range(0, parentSpawn.childCount)).transform.position, Quaternion.identity);
    }

    public GameObject GetShoot()
    {
        return shoot;
    }

    public float GetCD()
    {
        return CD;
    }

    public void SetBulletNumber(int addValue)
    {
        shootStockActual += addValue;
    }

    public int GetBulletNumber()
    {
        return shootStockActual;
    }

    public bool GetDefending()
    {
        return isDefending;
    }

    public void SetDefending(bool newDefending)
    {
        isDefending = newDefending;
    }

    public GameObject GetAstral()
    {
        return astral;
    }

    public void LooseTime(float addTimeValue)
    {
        timeActual -= addTimeValue;
    }
}
