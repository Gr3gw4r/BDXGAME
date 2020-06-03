using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    public GameObject[] bonusObjects;

    public Transform bonusSpawnPoint;

    public GameObject destroyedParticles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void isBroke()
    {
        Instantiate(bonusObjects[Random.Range(0, bonusObjects.Length)], bonusSpawnPoint.position, Quaternion.identity);
        Instantiate(destroyedParticles, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}