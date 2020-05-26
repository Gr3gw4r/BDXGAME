using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptysScript : MonoBehaviour
{
    public GameObject effectiveParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetParticles(bool newState)
    {
        effectiveParticles.SetActive(newState);
    }
}
