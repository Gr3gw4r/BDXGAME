using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointSorciereScript : MonoBehaviour
{
    private bool isFree = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFreeState(bool newState)
    {
        isFree = newState;
    }

    public bool GetFreeState()
    {
        return isFree;
    }
}
