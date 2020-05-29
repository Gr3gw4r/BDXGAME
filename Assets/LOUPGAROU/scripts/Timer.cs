using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance;

    public GameObject textDisplay;
    public int secondsLeft = 30;
    public bool takingAway = false;

    public Transform target;

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

    private void Start()
    {
        textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
    }

    private void Update()
    {
        if(takingAway == false && secondsLeft > 0)
        {
            StartCoroutine(TimerTake());
        }

        if(secondsLeft <= 0)
        {
            GameManager.Instance.AddTotalScore(100);
            StartCoroutine(GameManager.Instance.DeathScreen(gamemodes.LoupGarou, 100));
        }
    }

    IEnumerator TimerTake()
    {
        takingAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if(secondsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;
        }
        else
        {
            textDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        }
        takingAway = false;
    }

    public void Heal()
    {
        secondsLeft += 10;
        Debug.Log("+10sec");
    }

    public Transform GetTarget()
    {
        return target;
    }
}
