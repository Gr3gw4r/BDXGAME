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
    public GameObject beforeStartObject;

    public float time;
    private float timeActual;

    public MGhostObject[] objectsToSpawnMGhost;
    private GameObject objectToSpawn;
    public SpriteRenderer[] emptys;

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

    private bool gaveScore;

    public float timeRandomLine;
    private float timeRandomLineActual;

    public float randomTimeToVoiceLine;

    public string[] randomVoiceLine;

    private bool voiceLineTime = false;

    public string startVoiceLine;
    private bool startVoiceLineDid = false;

    public string lowTimeVoiceLine;
    private bool lowTimeVoiceLineDid = false;

    public string[] badObjectVoiceLine;
    public string[] goodObjectVoiceLine;

    public string photoMadeVoiceLine;

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
        GameManager.Instance.SetRunMode(runmodes.single);
        GameManager.Instance.AddGamesMade();

        timeActual = time;

        timeRandomLineActual = timeRandomLine + Random.Range(-randomTimeToVoiceLine, randomTimeToVoiceLine);
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRandomLineActual > 0)
        {
            timeRandomLineActual -= Time.deltaTime;

            if (timeRandomLineActual <= 0)
            {
                AudioManager.Instance.PlaySound(randomVoiceLine[Random.Range(0, randomVoiceLine.Length)]);
                timeRandomLineActual = timeRandomLine + Random.Range(-randomTimeToVoiceLine, randomTimeToVoiceLine);
            }
        }

        if (timeActual <= 0)
        {
            if (gaveScore == false)
            {
                GameManager.Instance.DeathScreen(gamemodes.LoupGarou, score);
                gaveScore = true;
            }

            StartCoroutine(GameManager.Instance.DeathScreen(gamemodes.Fantome, score));
        }
        else
        {
            if (isStarted == true)
            {
                timeActual -= Time.deltaTime;
                CDFGhostActual += Time.deltaTime;

                if (timeActual <= 20)
                {
                    if (lowTimeVoiceLineDid == false)
                    {
                        AudioManager.Instance.PlaySound(lowTimeVoiceLine);
                        lowTimeVoiceLineDid = true;
                    }
                }
            }
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
        if (startVoiceLineDid == false)
        {
            AudioManager.Instance.PlaySound(startVoiceLine);
            startVoiceLineDid = true;
        }

        isStarted = true;
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
        }
    }

    public void CheckGhostObject(GameObject myGameObject)
    {
        if (isEffectiveEmptys == true)
        {
            if (myGameObject == objectToSpawn)
            {
                AudioManager.Instance.PlaySound(goodObjectVoiceLine[Random.Range(0, goodObjectVoiceLine.Length)]);
                Instantiate(goodObjectParticles, myGameObject.transform.position, Quaternion.identity);
                SpawnMGhost();
            }
            else if (myGameObject != objectToSpawn)
            {
                AudioManager.Instance.PlaySound(badObjectVoiceLine[Random.Range(0, badObjectVoiceLine.Length)]);
                Instantiate(badObjectParticles, myGameObject.transform.position, Quaternion.identity);
                Invoke("SetEmptys", timeFrozenEmptys);
            }

            isEffectiveEmptys = false;
            SetEmptysParticles(false);
        }
    }

    public void AddScore(int newScore)
    {
        if (Random.Range(0, 10f) > 7)
        {
            AudioManager.Instance.PlaySound(photoMadeVoiceLine);
        }

        score += newScore;
        GameManager.Instance.AddTotalScore(newScore);
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
        scoreText.text = ("Score:") + score.ToString("0");
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