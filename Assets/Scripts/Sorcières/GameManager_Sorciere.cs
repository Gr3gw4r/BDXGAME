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

    public GameObject astral;

    public GameObject soldier;
    public Transform parentSpawnSoldier;

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
        CDSpawnActualSoldier += Time.deltaTime;

        if (CDSpawnActualSoldier >= CDSpawnSoldier)
        {
            SpawnEnnemies(soldier, parentSpawnSoldier);
            CDSpawnSoldier = CDSpawnSoldier - (CDSpawnSoldier * (difficultyIndex / 100));
            Debug.Log(CDSpawnSoldier);
            CDSpawnActualSoldier = 0;
        }
    }

    private void SpawnEnnemies(GameObject newEnnemy, Transform newTransformParent)
    {
        GameObject EnnemySpawned = Instantiate(newEnnemy, newTransformParent.GetChild(Random.Range(0, newTransformParent.childCount)).transform.position, Quaternion.identity);
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
}