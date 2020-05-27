using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySorciereScript : MonoBehaviour
{
    private Transform mySpawnPoint;
    public int scoreValue;

    public float life;
    private float lifeActual;
    private bool isImmune = false;

    public bool isPriest;
    public GameObject shieldParticles;

    // Start is called before the first frame update
    void Start()
    {
        lifeActual = life;

        if (isPriest == true)
        {
            GameManager_Sorciere.Instance.SetPriestNumber(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(GameManager_Sorciere.Instance.GetPlayer().transform.position.x, transform.position.y, GameManager_Sorciere.Instance.GetPlayer().transform.position.z));

        if (life <= 0)
        {
            if (isPriest == true)
            {
                GameManager_Sorciere.Instance.SetPriestNumber(-1);
            }

            Destroy(this.gameObject);
        }

        if (isPriest == true)
        {
            if (GetImmune() == true)
            {
                shieldParticles.SetActive(true);
            }
            else
            {
                shieldParticles.SetActive(false);
            }
        }
    }

    public void SetMySpawnPoint(Transform newSpawnPoint)
    {
        mySpawnPoint = newSpawnPoint;
        mySpawnPoint.GetComponent<SpawnPointSorciereScript>().SetFreeState(false);
    }

    private void OnDestroy()
    {
        if (mySpawnPoint != null)
        {
            mySpawnPoint.GetComponent<SpawnPointSorciereScript>().SetFreeState(true);
        }

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