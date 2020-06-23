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

        int[] myHighScores = GameManager.Instance.getHighScore(myGM);

        if (isEntire == true)
        {
            myHighScores = GameManager.Instance.GetHighTotalScore();
        }
        else
        {
            myHighScores = GameManager.Instance.getHighScore(myGM);
        }

        for (int k = 0; k < transform.childCount && k < myHighScores.Length; k ++)
        {
            if (myHighScores[k] != 0)
            {
                transform.GetChild(k).GetComponent<Text>().text = myHighScores[k].ToString("0");
            }
            else
            {
                transform.GetChild(k).GetComponent<Text>().text = ("");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
