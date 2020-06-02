using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathSceneManager : MonoBehaviour
{
    public GameObject fullRunObject;
    public GameObject singleRunObject;
    public GameObject storyRunObject;

    private runmodes myRunmode;

    public GameObject TPObjectNextScene;

    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI HighTotalScoreText;

    public Transform gamesScore;

    public TextMeshProUGUI[] GamesScoreText;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SetRunMode(runmodes.full);
        myRunmode = GameManager.Instance.GetRunMode();

        switch (myRunmode)
        {
            case runmodes.full:
                fullRunObject.SetActive(true);
                totalScoreText.text = ("Score total: ") + GameManager.Instance.GetTotalScore().ToString("0");
                HighTotalScoreText.text = ("Meilleur score total: ") + GameManager.Instance.GetHighTotalScore().ToString("0");
                TPObjectNextScene.GetComponent<TPObjectScript>().SetMyDestination(GameManager.Instance.GetNextDestination());
                break;
            case runmodes.single:
                singleRunObject.SetActive(true);
                break;
            case runmodes.story:
                storyRunObject.SetActive(true);
                break;
            default:
                break;
        }

        if (GameManager.Instance.GetGamesMadeNumber() >= 1)
        {
            for (int i = 1; i <= GameManager.Instance.GetGamesMadeNumber(); i++)
            {
                gamesScore.GetChild(i - 1).gameObject.SetActive(true);
                GamesScoreText[i - 1].text = GameManager.Instance.GetGamesScore(i - 1).ToString("0");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
