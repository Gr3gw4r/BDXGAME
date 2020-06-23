using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager_LG2 : MonoBehaviour
{

    public static GameManager_LG2 Instance;

    public Transform spawnPointsParent;
    public Transform spawnPointsPNJParent;

    public GameObject player;

    private List<Transform> spawnPoints = new List<Transform>();
    private List<Transform> spawnPointsPNJ = new List<Transform>();

    public Slider timeSlider;
    public float time;
    private float timeActual;

    public GameObject target;
    public Transform camera;

    public GameObject barrelObject;
    public int barrelNumber;

    public int PNJNumber;

    private int score = 0;
    public TextMeshProUGUI scoreText;

    private bool gaveScore = false;

    private int multiplierScore = 1;
    public float multiplierScoreDuration;
    private float multiplierScoreDurationActual = 0;

    public float timeRandomLine;
    private float timeRandomLineActual;

    public float randomTimeToVoiceLine;

    public string[] randomVoiceLine;

    private bool voiceLineTime = false;

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

    // Start is called before the first frame update
    void Start()
    {
        timeActual = time;

        GameManager.Instance.SetGameMode(gamemodes.LoupGarou);
        GameManager.Instance.SetRunMode(runmodes.single);
        GameManager.Instance.AddGamesMade();

        ShowScore();
        ShowTime();

        SpawnBarrels();
        SpawnPNJ();

        AudioManager.Instance.StopAllSound();

        timeRandomLineActual = timeRandomLine + Random.Range(-randomTimeToVoiceLine, randomTimeToVoiceLine);
    }

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

        if (time > 0)
        {
            timeActual -= Time.deltaTime;
            ShowTime();
        }

        //textDisplay.text = time.ToString("0");

        if (timeActual <= 0)
        {
            if (gaveScore == false)
            {
                GameManager.Instance.DeathScreen(gamemodes.LoupGarou, score);
                gaveScore = true;
            }

            StartCoroutine(GameManager.Instance.DeathScreen(gamemodes.LoupGarou, score));
        }

        if (multiplierScore > 1)
        {
            multiplierScoreDurationActual += Time.deltaTime;

            if (multiplierScoreDurationActual >= multiplierScoreDuration)
            {
                multiplierScore = 1;
            }
        }
    }

    private void SpawnPNJ()
    {
        for (int i = 0; i < spawnPointsPNJParent.childCount; i ++)
        {
            spawnPoints.Add(spawnPointsPNJParent.GetChild(i).transform);
        }

        for (int k = 0; k < PNJNumber; k ++)
        {
            int spawnPointIndex = Random.Range(0, spawnPointsPNJParent.childCount);

            spawnPointsPNJParent.GetChild(spawnPointIndex).GetComponent<PNJSpawnerScript>().SpawnPNJ();

            spawnPointsPNJ.Remove(spawnPointsPNJParent.GetChild(spawnPointIndex));
        }
    }

    private void SpawnBarrels()
    {
        for (int i = 0; i < spawnPointsParent.childCount; i++)
        {
            spawnPoints.Add(spawnPointsParent.GetChild(i).transform);
        }

        for (int k = 0; k < barrelNumber; k++)
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Count);

            Instantiate(barrelObject, spawnPoints[spawnPointIndex].transform.position, Quaternion.identity);

            spawnPoints.Remove(spawnPoints[spawnPointIndex]);
        }
    }

    public void Heal()
    {
        timeActual += 10;
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public Transform GetCamera()
    {
        return camera;
    }

    public GameObject GetBarrel()
    {
        return barrelObject;
    }

    public int GetMultiplier()
    {
        return multiplierScore;
    }

    public int GetScore()
    {
        return score;
    }

    public void AddScore(int addValue)
    {
        score += addValue * multiplierScore;
        GameManager.Instance.AddTotalScore(addValue * multiplierScore);
        Debug.Log(score);
        Debug.Log(GameManager.Instance.GetTotalScore());
        ShowScore();
    }

    public void AddMultiplier()
    {
        multiplierScoreDurationActual = 0;

        if (multiplierScore < 6)
        {
            multiplierScore++;
        }
    }

    private void ShowScore()
    {
        scoreText.text = score.ToString("0");
    }

    private void ShowTime()
    {
        timeSlider.value = timeActual / time;
    }
}