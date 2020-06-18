using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetHighScoreToUI : MonoBehaviour
{
    public gamemodes myGM;
    private Text myText;

    public bool isEntire;

    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();

        int myHighScore = GameManager.Instance.getHighScore(myGM);

        if (isEntire == true)
        {
            myHighScore = GameManager.Instance.GetTotalScore();
        }
        else
        {
            myHighScore = GameManager.Instance.getHighScore(myGM);
        }

        if (myHighScore != 0)
        {
            myText.text = myHighScore.ToString("0");
        }
        else
        {
            myText.text = ("");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
