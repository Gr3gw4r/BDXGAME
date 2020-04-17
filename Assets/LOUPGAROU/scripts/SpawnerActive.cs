using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerActive : MonoBehaviour
{
    public GameObject spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spawner.SetActive(true);
            Debug.Log("good");
        }
        else
        {
            spawner.SetActive(false);
        }
    }
}
