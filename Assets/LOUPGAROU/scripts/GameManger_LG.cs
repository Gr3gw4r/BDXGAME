using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger_LG : MonoBehaviour
{
    public static GameManger_LG Instance;

    public float fightAreaPercent;
    public float bonusAreaPercent;

    public List<GameObject> TPPoints;

    public GameObject player;

    public float distanceToTriggerTP;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> TPPoints = new List<GameObject>();

        SetPoints();

        GameManager.Instance.SetGameMode(gamemodes.LoupGarou);
        GameManager.Instance.SetRunMode(runmodes.single);
        GameManager.Instance.AddGamesMade();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetPoints()
    {
        Debug.Log(GameObject.FindGameObjectsWithTag("TPPoint"));

        foreach (GameObject myTPPoint in GameObject.FindGameObjectsWithTag("TPPoint"))
        {
            Debug.Log("salut Tom");
            TPPoints.Add(myTPPoint);
        }

        int TPNumber = TPPoints.Count;

        Debug.Log("On a " + TPNumber + " point de TP");

        int fightAreaCount = Mathf.RoundToInt(TPNumber * fightAreaPercent);
        int bonusAreaCount = Mathf.RoundToInt(TPNumber * bonusAreaPercent);

        for (int i = 0; i < TPNumber; i ++)
        {
            int myIndex = Random.Range(0, TPPoints.Count);

            if (fightAreaCount > 0)
            {
                TPPoints[myIndex].GetComponent<TPPointScript>().SetMyArea(Area.Fight);
                fightAreaCount--;
            }
            else
            {
                if (bonusAreaCount > 0)
                {
                    TPPoints[myIndex].GetComponent<TPPointScript>().SetMyArea(Area.Bonus);
                    bonusAreaCount--;
                }
            }

            TPPoints.Remove(TPPoints[myIndex]);
        }
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public float GetMinDistance()
    {
        return distanceToTriggerTP;
    }
}