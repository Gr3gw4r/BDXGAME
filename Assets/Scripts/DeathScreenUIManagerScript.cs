using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScreenUIManagerScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        int myScore = GameManager.Instance.getLastScore();
        int myHighscore = GameManager.Instance.getHighScore();
        scoreText.text = ("Score: ") + myScore.ToString("0");
        highscoreText.text = ("Meilleur Score: ") + myScore.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
