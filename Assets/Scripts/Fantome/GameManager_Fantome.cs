using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class MGhostObject
{
    public GameObject myObject;
    public Sprite[] myPictures;
    private bool isShowing = false;
}

public class GameManager_Fantome : MonoBehaviour
{
    private bool isStarted = false;
    public GameObject teleporting;
    public GameObject beforeStartObject;

    public float time;
    private float timeActual;

    public MGhostObject[] objectsToSpawnMGhost;
    private GameObject objectToSpawn;
    public SpriteRenderer[] emptys;

    public float resetCD;
    private float CDActual = 0;

    private int score = 0;

    public static GameManager_Fantome Instance;

    public GameObject mGhost;
    public Transform parentSpawnPointM;
    public Transform parentSpawnPointF;

    public GameObject fGhost;
    public float CDFGhost;
    private float CDFGhostActual = 0;

    private bool MGhostInScene = false;

    public GameObject ghostCamera;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI TimeText;

    public GameObject goodObjectParticles;
    public GameObject badObjectParticles;

    private bool isEffectiveEmptys = true;
    public float timeFrozenEmptys;
    public float timeToCatchMGhost;

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
        SetEmptys();

        GameManager.Instance.SetGameMode(gamemodes.Fantome);

        timeActual = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeActual <= 0)
        {
            StartCoroutine(GameManager.Instance.DeathScreen(gamemodes.Fantome, score));
        }
        else
        {
            if (isStarted == true)
            {
                timeActual -= Time.deltaTime;
                CDActual += Time.deltaTime;
                CDFGhostActual += Time.deltaTime;
            }
        }

        if (CDActual >= resetCD)
        {
            SetEmptys();
        }

        if (CDFGhostActual >= CDFGhost)
        {
            SpawnFGhost();
            CDFGhostActual = 0;
        }

        

        ShowStats();
    }

    public void GetStarted()
    {
        isStarted = true;
        teleporting.SetActive(true);
        beforeStartObject.SetActive(false);
    }

    public GameObject GetCamera()
    {
        return ghostCamera;
    }

    public void TakeObject(GameObject myObject)
    {

    }

    public void SetEmptys()
    {
        isEffectiveEmptys = true;
        SetEmptysParticles(true);

        if (MGhostInScene == false)
        {
            int indexToSpawn = Random.Range(0, objectsToSpawnMGhost.Length);
            objectToSpawn = objectsToSpawnMGhost[indexToSpawn].myObject;

            for (int i = 0; i < emptys.Length; i++)
            {
                emptys[i].sprite = objectsToSpawnMGhost[indexToSpawn].myPictures[i];
            }

            CDActual = 0;
        }
    }

    public void CheckGhostObject(GameObject myGameObject)
    {
        if (isEffectiveEmptys == true)
        {
            if (myGameObject == objectToSpawn)
            {
                Instantiate(goodObjectParticles, myGameObject.transform.position, Quaternion.identity);
                SpawnMGhost();
                CDActual -= timeToCatchMGhost;
            }
            else if (myGameObject != objectToSpawn)
            {
                Instantiate(badObjectParticles, myGameObject.transform.position, Quaternion.identity);
                Invoke("SetEmptys", timeFrozenEmptys);
            }

            isEffectiveEmptys = false;
            SetEmptysParticles(false);
        }
    }

    public void AddScore(int newScore)
    {
        score += newScore;
    }

    public void SpawnMGhost()
    {
        if (MGhostInScene == false)
        {
            int mySpawnPointIndex = Random.Range(0, parentSpawnPointM.childCount);
            GameObject newGhost = Instantiate(mGhost, parentSpawnPointM.GetChild(mySpawnPointIndex).transform.position, Quaternion.identity);
            newGhost.transform.rotation = parentSpawnPointM.GetChild(mySpawnPointIndex).transform.rotation;
        }
    }

    public void SpawnFGhost()
    {
        if (MGhostInScene == false)
        {
            Instantiate(fGhost, parentSpawnPointF.GetChild(Random.Range(0, parentSpawnPointF.childCount)).transform.position, Quaternion.identity);
        }
    }

    public void setMGhostInScene(bool newMGhostInScene)
    {
        MGhostInScene = newMGhostInScene;
    }

    public GameObject GetObjectToSpawn()
    {
        return objectToSpawn;
    }

    public GameObject GetGhostCamera()
    {
        return ghostCamera;
    }

    private void ShowStats()
    {
        scoreText.text = ("Score:") + score.ToString(" 0");
        TimeText.text = ((timeActual/time)*100).ToString("0") + ("%");
    }

    public void SetEmptysParticles(bool newState)
    {
        for (int i = 0; i < emptys.Length; i++)
        {
            emptys[i].GetComponent<EmptysScript>().SetParticles(newState);
        }
    }
}