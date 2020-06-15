using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LookAtPlayerScript : MonoBehaviour
{
    private GameObject player;
    public TextMeshProUGUI myScore;
    public TextMeshProUGUI myMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerMovement.Instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = player.transform.position;
        target.y = transform.position.y;
        transform.LookAt(target);
        transform.Rotate(0, 180, 0);
    }

    public void SetMyScoreText(string newScoreText)
    {
        myScore.text = newScoreText;
    }

    public void SetMyMultiplierText(string newMultiplierText)
    {
        myMultiplier.text = ("X") + newMultiplierText;
    }
}