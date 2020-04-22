using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemySorciereScript : MonoBehaviour
{
    private Transform mySpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMySpawnPoint(Transform newSpawnPoint)
    {
        mySpawnPoint = newSpawnPoint;
        mySpawnPoint.GetComponent<SpawnPointSorciereScript>().SetFreeState(false);
    }

    private void OnDestroy()
    {
        mySpawnPoint.GetComponent<SpawnPointSorciereScript>().SetFreeState(true);
    }
}
