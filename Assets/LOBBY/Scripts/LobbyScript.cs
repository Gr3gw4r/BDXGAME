using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.SetRunMode(runmodes.story);
        GameManager.Instance.ResetGamesMade();
        GameManager.Instance.ResetTotalScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
