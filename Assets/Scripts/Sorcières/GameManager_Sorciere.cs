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
    public float CDsoldier;

    public int shootStock;
    private int shootStockActual;

    private bool isDefending = false;

    public float CDSpawnSoldier;
    private float CDSpawnActualSoldier = 0;
    public float difficultyIndex;

    public float CDSpawnPriest;
    private float CDSpawnPriestActual = 0;

    public GameObject astral;

    public GameObject soldier;
    public Transform parentSpawnSoldier;

    public GameObject priest;
    public Transform parentSpawnPriest;

    private int priestNumber;
    public float timePaused;
    private float timePausedActual = 0;
    private bool isPaused = false;
    private bool pauseAllowed = false;

    public GameObject bossObject;

    public float minTimeToSpawnBoss;
    public float maxTimeToSpawnBoss;
    private float timeToSpawnBoss;
    private bool bossIsSpawned = false;

    private float myScore = 0;

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

        timeToSpawnBoss = Random.Range(minTimeToSpawnBoss, maxTimeToSpawnBoss);
    }

    // Update is called once per frame
    void Update()
    {
        timeActual -= Time.deltaTime;
        CDSpawnActualSoldier += Time.deltaTime;
        CDSpawnPriestActual += Time.deltaTime;
        timeToSpawnBoss -= Time.deltaTime;

        if ((priestNumber <= 0) && (pauseAllowed == true))
        {
            isPaused = true;
            timePausedActual = 0;
            pauseAllowed = false;
        }

        if ((timeToSpawnBoss <= 0) && (bossIsSpawned == false))
        {
            SpawnEnnemies(bossObject, parentSpawnSoldier);
            bossIsSpawned = true;
        }

        if (isPaused)
        {
            timePausedActual += Time.deltaTime;

            if (timePausedActual >= timePaused)
            {
                isPaused = false;
                pauseAllowed = true;
            }
        }

        if (timeActual <= 0)
        {
            Debug.Log("ah yes");
        }

        if (CDSpawnActualSoldier >= CDSpawnSoldier)
        {
            SpawnEnnemies(soldier, parentSpawnSoldier);
            CDSpawnSoldier = CDSpawnSoldier - (CDSpawnSoldier * (difficultyIndex / 100));
            CDSpawnActualSoldier = 0;
        }

        if (CDSpawnPriestActual >= CDSpawnPriest)
        {
            SpawnEnnemies(priest, parentSpawnPriest);
            CDSpawnPriest = CDSpawnPriest - (CDSpawnPriest * (difficultyIndex / 100));
            Debug.Log(CDSpawnPriest);
            CDSpawnPriestActual = 0;
        }
    }

    private void SpawnEnnemies(GameObject newEnnemy, Transform newTransformParent)
    {
        List<Transform> spawnPointTransformList = new List<Transform>();
        bool spawnIsPossible = false;

        for (int i = 0; i < newTransformParent.childCount; i ++)
        {
            if (newTransformParent.GetChild(i).GetComponent<SpawnPointSorciereScript>().GetFreeState())
            {
                spawnPointTransformList.Add(newTransformParent.GetChild(i));
                spawnIsPossible = true;
            }
        }

        if (spawnIsPossible)
        {
            Transform mySpawnPoint = spawnPointTransformList[Random.Range(0,spawnPointTransformList.Count)];
            GameObject EnnemySpawned = Instantiate(newEnnemy, mySpawnPoint.position, Quaternion.identity);
            EnnemySpawned.GetComponent<EnnemySorciereScript>().SetMySpawnPoint(mySpawnPoint);
        }
    }

    public GameObject GetShoot()
    {
        return shoot;
    }

    public float GetCD()
    {
        return CDsoldier;
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

    public void SetPriestNumber(int AddValue)
    {
        priestNumber += AddValue;
    }

    public bool GetPriestFree()
    {
        for (int i = 0; i < parentSpawnSoldier.childCount; i++)
        {
            if (parentSpawnSoldier.GetChild(i).GetComponent<SpawnPointSorciereScript>().GetFreeState() == false)
            {
                return false;
            }
        }

        return true;
    }

    public void GetScore(float addValue)
    {
        myScore = addValue;
    }
}