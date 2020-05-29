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

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SetRunMode(runmodes.full);
        myRunmode = GameManager.Instance.GetRunMode();

        switch (myRunmode)
        {
            case runmodes.full:
                fullRunObject.SetActive(true);
                totalScoreText.text = GameManager.Instance.GetTotalScore().ToString("0");
                HighTotalScoreText.text = GameManager.Instance.GetHighTotalScore().ToString("0");
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
