using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySorciereScript : MonoBehaviour
{
    private Transform mySpawnPoint;
    public float scoreValue;

    public float life;
    private float lifeActual;
    private bool isImmune = false;

    public bool isPriest;

    // Start is called before the first frame update
    void Start()
    {
        lifeActual = life;
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetMySpawnPoint(Transform newSpawnPoint)
    {
        mySpawnPoint = newSpawnPoint;
        mySpawnPoint.GetComponent<SpawnPointSorciereScript>().SetFreeState(false);
    }

    private void OnDestroy()
    {
        mySpawnPoint.GetComponent<SpawnPointSorciereScript>().SetFreeState(true);
        GameManager_Sorciere.Instance.GetScore(scoreValue);
    }

    public void LooseLife(float addValue)
    {
        if (isPriest == true)
        {
            if (GetImmune() == false)
            {
                life -= addValue;
            }
        }
        else
        {
            life -= addValue;
        }
    }

    public bool GetImmune()
    {
        if (GameManager_Sorciere.Instance.GetPriestFree() == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}