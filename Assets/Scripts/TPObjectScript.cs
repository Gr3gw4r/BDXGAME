using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TPObjectScript : MonoBehaviour
{
    public gamemodes myDestination;

    public GameObject tpParticles;

    public bool lastGamemode;

    public GameObject hubObject;
    public GameObject WolfObject;
    public GameObject WitchObject;
    public GameObject GhostObject;
    public GameObject menuObject;

    public TextMeshProUGUI myText;

    public string hubText;
    public string replayText;
    public string witchText;
    public string ghostText;
    public string werewolfText;
    public string MenuText;

    // Start is called before the first frame update
    void Start()
    {
        if (lastGamemode == true)
        {
            myDestination = GameManager.Instance.GetGameMode();
            myText.text = replayText;
        }

        if (myDestination == gamemodes.Hub)
        {
            myText.text = hubText;
            hubObject.SetActive(true);
        }

        if (myDestination == gamemodes.Sorciere)
        {
            myText.text = witchText;
            WitchObject.SetActive(true);
        }

        if (myDestination == gamemodes.Fantome)
        {
            myText.text = ghostText;
            GhostObject.SetActive(true);
        }

        if (myDestination == gamemodes.LoupGarou)
        {
            myText.text = werewolfText;
            WolfObject.SetActive(true);
        }

        if (myDestination == gamemodes.Menu)
        {
            myText.text = MenuText;
            menuObject.SetActive(true);
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

    public void SetMyDestination(gamemodes newGamemode)
    {
        myDestination = newGamemode;
    }
}
