using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Area { Bonus, Fight, Empty}

public class TPPointScript : MonoBehaviour
{
    public Area MyArea = Area.Empty;

    private bool isActivated = false;
    private GameObject player;

    private float minDistance;

    public Transform[] spawnPoints;

    private GameObject barrel;

    // Start is called before the first frame update
    void Start()
    {
        minDistance = GameManger_LG.Instance.GetMinDistance();
        player = GameManger_LG.Instance.GetPlayer();

        barrel = GameManger_LG.Instance.GetBarrel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < minDistance)
        {
            Triggered();
        }
    }

    public void SetMyArea(Area newArea)
    {
        MyArea = newArea;
    }

    public void Triggered()
    {
        if (isActivated == false)
        {
            switch (MyArea)
            {
                case Area.Bonus:
                    Instantiate(barrel, transform.position, Quaternion.identity);
                    break;
                case Area.Fight:
                    WaveSpawner.Instance.SpawnEnemy(GetSpawnPoints());
                    break;
                case Area.Empty:
                    break;
                default:
                    break;
            }

            isActivated = true;
        }
    }

    public Transform[] GetSpawnPoints()
    {
        return spawnPoints;
    }
}
