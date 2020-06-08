using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Area { Bonus, Fight, Empty}

public class TPPointScript : MonoBehaviour
{
    public Area MyArea = Area.Empty;

    private bool isActivated = false;
    private GameObject player;

    public float minDistanceToGetSpawner;
    private float minDistance;

    private List<GameObject> spawnPoints = new List<GameObject>();

    private GameObject barrel;
    public Transform barrelSpawnParent;

    private Transform[] newSpawnPoint;

    private bool gotPlayer = false;

    void Start()
    {
        minDistance = GameManger_LG.Instance.GetMinDistance();
        player = GameManger_LG.Instance.GetPlayer();

        barrel = GameManger_LG.Instance.GetBarrel();

        InitialiseMe();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < minDistance)
        {
            Triggered();
            gotPlayer = true;
        }
        else
        {
            gotPlayer = false;
        }
    }

    public void SetMyArea(Area newArea)
    {
        MyArea = newArea;
    }

    public void Triggered()
    {
        foreach (GameObject mySpawnPoints in GameObject.FindGameObjectsWithTag("TPPoint"))
        {
            if (mySpawnPoints.GetComponent<TPPointScript>().GotPlayerAsk() == false)
            {
                if (Vector3.Distance(transform.position, mySpawnPoints.transform.position) < minDistanceToGetSpawner)
                {
                    spawnPoints.Add(mySpawnPoints);
                }
            }
        }

        if (isActivated == false)
        {
            switch (MyArea)
            {
                case Area.Bonus:
                    Debug.Log("salut");
                    break;
                case Area.Fight:
                    Debug.Log(spawnPoints.Count);
                    WaveSpawner.Instance.SpawnEnemy(spawnPoints);
                    break;
                case Area.Empty:
                    break;
                default:
                    break;
            }

            isActivated = true;
        }
    }

    public void InitialiseMe()
    {
        switch (MyArea)
        {
            case Area.Bonus:
                Instantiate(barrel, barrelSpawnParent.GetChild(Random.Range(0, barrelSpawnParent.childCount)).transform.position , Quaternion.identity);
                break;
            case Area.Fight:
                break;
            case Area.Empty:
                break;
            default:
                break;
        }
    }

    public bool GotPlayerAsk()
    {
        return gotPlayer;
    }
}
