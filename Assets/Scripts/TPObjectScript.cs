using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPObjectScript : MonoBehaviour
{
    public gamemodes myDestination;

    public GameObject tpParticles;

    public bool lastGamemode;

    public GameObject hubObject;
    public GameObject WolfObject;
    public GameObject WitchObject;
    public GameObject GhostObject;

    // Start is called before the first frame update
    void Start()
    {
        if (lastGamemode == true)
        {
            myDestination = GameManager.Instance.GetGameMode();
        }

        if (myDestination == gamemodes.Hub)
        {
            hubObject.SetActive(true);
        }

        if (myDestination == gamemodes.Sorciere)
        {
            WitchObject.SetActive(true);
        }

        if (myDestination == gamemodes.Fantome)
        {
            GhostObject.SetActive(true);
        }

        if (myDestination == gamemodes.LoupGarou)
        {
            WolfObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public gamemodes getDestination()
    {
        Instantiate(tpParticles, transform.position, Quaternion.identity);

        return myDestination;
    }
}
