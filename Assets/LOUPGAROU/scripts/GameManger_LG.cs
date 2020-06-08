using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManger_LG : MonoBehaviour
{
    public static GameManger_LG Instance;

    public float fightAreaPercent;
    public float bonusAreaPercent;

    public List<GameObject> TPPoints;

    public GameObject player;

    public float distanceToTriggerTP;

    public Slider timeSlider;
    public float time;
    private float timeActual;

    public Transform target;

    public GameObject barrelObject;

    private int score = 0;
    public TextMeshProUGUI scoreText;

    private bool gaveScore = false;

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

        List<GameObject> TPPoints = new List<GameObject>();

        SetPoints();

        GameManager.Instance.SetGameMode(gamemodes.LoupGarou);
        GameManager.Instance.SetRunMode(runmodes.single);
        GameManager.Instance.AddGamesMade();

        ShowScore();
        ShowTime();
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    private void SetPoints()
    {
        foreach (GameObject myTPPoint in GameObject.FindGameObjectsWithTag("TPPoint"))
        {
            TPPoints.Add(myTPPoint);
        }

        int TPNumber = TPPoints.Count;

        Debug.Log("On a " + TPNumber + " point de TP");

        int fightAreaCount = Mathf.RoundToInt(TPNumber * fightAreaPercent);
        int bonusAreaCount = Mathf.RoundToInt(TPNumber * bonusAreaPercent);

        for (int i = 0; i < TPNumber; i ++)
        {
            int myIndex = Random.Range(0, TPPoints.Count);

            if (fightAreaCount > 0)
            {
                TPPoints[myIndex].GetComponent<TPPointScript>().SetMyArea(Area.Fight);
                fightAreaCount--;
            }
            else
            {
                if (bonusAreaCount > 0)
                {
                    TPPoints[myIndex].GetComponent<TPPointScript>().SetMyArea(Area.Bonus);
                    bonusAreaCount--;
                }
            }

            TPPoints[myIndex].GetComponent<TPPointScript>().InitialiseMe();

            TPPoints.Remove(TPPoints[myIndex]);
        }
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public float GetMinDistance()
    {
        return distanceToTriggerTP;
    }

    public void Heal()
    {
        timeActual += 10;
    }

    public Transform GetTarget()
    {
        return target;
    }

    public GameObject GetBarrel()
    {
        return barrelObject;
    }

    public void AddScore(int addValue)
    {
        score += addValue;
        ShowScore();
    }

    private void ShowScore()
    {
        scoreText.text = score.ToString("0");
    }

    private void ShowTime()
    {
        timeSlider.value = timeActual/time;
    }
}