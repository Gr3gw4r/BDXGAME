using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerActive : MonoBehaviour
{
    public GameObject spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WaveSpawn"))
        {
            GameObject == true; 
        }
        else
        {

        }
    }
}
