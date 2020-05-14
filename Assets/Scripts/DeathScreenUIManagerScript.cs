using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScreenUIManagerScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        int myScore = GameManager.Instance.getLastScore();
        scoreText.text = myScore.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
