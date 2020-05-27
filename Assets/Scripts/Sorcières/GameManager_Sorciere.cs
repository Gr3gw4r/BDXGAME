using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum spells { Attack, Defend, TimeSlow}

public class GameManager_Sorciere : MonoBehaviour
{
    public static GameManager_Sorciere Instance;

    public GameObject player;

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

    public GameObject soldier;
    public Transform parentSpawnSoldier;

    public GameObject priest;
    public Transform parentSpawnPriest;

    private int priestNumber = 0;
    public float timePaused;
    private float timePausedActual = 0;
    private bool isPaused = false;
    private bool pauseAllowed = true;
    private bool firstPriest = false;

    public GameObject bossObject;

    public float minTimeToSpawnBoss;
    public float maxTimeToSpawnBoss;
    private float timeToSpawnBoss;
    private bool bossIsSpawned = false;

    private int myScore = 0;

    public Image timeImage;
    public TextMeshProUGUI scoreText;

    public Transform playerHead;

    private GameObject reloadObject;

    public ParticleSystem rightHandNoStock;

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

        ShowTime();
        ShowScore();

        GameManager.Instance.SetGameMode(gamemodes.Sorciere);
    }

    // Update is called once per frame
    void Update()
    {
        CDSpawnActualSoldier += Time.deltaTime;
        timeToSpawnBoss -= Time.deltaTime;

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
            StartCoroutine(GameManager.Instance.DeathScreen(gamemodes.Sorciere, myScore));
        }
        else
        {
            if (isPaused == false)
            {
                CDSpawnPriestActual += Time.deltaTime;
                timeActual -= Time.deltaTime;
                ShowTime();
            }
        }

        if (shootStockActual <= 0)
        {
            rightHandNoStock.gameObject.SetActive(false);
        }

        if (shootStockActual > 0)
        {
            var ps = rightHandNoStock.emission;
            ps.rateOverTime = shootStockActual;
            rightHandNoStock.gameObject.SetActive(true);
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
            CDSpawnPriestActual = 0;
        }

        if (isPaused)
        {
            timeImage.color = Color.yellow;
        }
        else
        {
            timeImage.color = Color.white;
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

    public void Reload()
    {
        shootStockActual = shootStock;
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

    public GameObject GetPlayer()
    {
        return player;
    }

    public void LooseTime(float addTimeValue)
    {
        timeActual -= addTimeValue;

        ShowTime();
    }

    public void SetPriestNumber(int AddValue)
    {
        priestNumber += AddValue;

        if ((priestNumber <= 0) && (pauseAllowed == true))
        {
            isPaused = true;
            timePausedActual = 0;
            pauseAllowed = false;
        }
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

    public void GetScore(int addValue)
    {
        myScore += addValue;
        ShowScore();
    }

    private void ShowTime()
    {
        timeImage.fillAmount = timeActual/timer;
    }

    private void ShowScore()
    {
        scoreText.text = ("Score : ") + myScore.ToString("0");
    }

    public Transform GetPlayerHead()
    {
        return playerHead;
    }

    public void SetReloadObject(GameObject myReloadObject)
    {
        reloadObject = myReloadObject;
    }

    public void DestroyReloadObject()
    {
        if (reloadObject != null)
        {
            Destroy(reloadObject);
        }
    }
}