using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LookAtPlayerScript : MonoBehaviour
{
    private Transform player;
    public TextMeshProUGUI myScore;
    public TextMeshProUGUI myMultiplier;

    public float distanceFromPNJ;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager_LG2.Instance.GetCamera();

        LookPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        LookPlayer();
    }

    public void SetMyScoreText(string newScoreText)
    {
        myScore.text = newScoreText;
    }

    public void SetMyMultiplierText(string newMultiplierText)
    {
        myMultiplier.text = ("X") + newMultiplierText;
    }

    public void Destroyed()
    {
        Destroy(gameObject);
    }

    private void LookPlayer()
    {
        Vector3 target = player.position;
        target.y = transform.position.y;
        transform.LookAt(target);
        transform.Rotate(0, 180, 0);
    }

    public void MoveMe(Vector3 ennemyPosition)
    {
        player = GameManager_LG2.Instance.GetCamera();
        Vector3 myDestination = ennemyPosition - player.position;
        myDestination.y = transform.position.y;
        Vector3 finalDestination = transform.position + (myDestination * distanceFromPNJ);
        finalDestination.y = transform.position.y;
        transform.position = finalDestination;
    }
}