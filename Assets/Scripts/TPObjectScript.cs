using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPObjectScript : MonoBehaviour
{
    public gamemodes myDestination;

    public GameObject tpParticles;

    public bool lastGamemode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public gamemodes getDestination()
    {
        Instantiate(tpParticles, transform.position, Quaternion.identity);

        if (lastGamemode == true)
        {
            return GameManager.Instance.GetGameMode();
        }
        else
        {
            return myDestination;
        }
    }
}
