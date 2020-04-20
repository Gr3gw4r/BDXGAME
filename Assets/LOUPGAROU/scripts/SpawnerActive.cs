using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerActive : MonoBehaviour
{
    public GameObject spawner;

    private void Start()
    {
        spawner.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            spawner.SetActive(true);
        }
    }
}
