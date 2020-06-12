using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNJSpawnerScript : MonoBehaviour
{
    public GameObject PNJObject;

    private Collider myCollider;

    private float myRadius;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();

        myRadius = (myCollider as SphereCollider).radius;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPNJ()
    {
        Instantiate(PNJObject, new Vector3(transform.position.x + Random.Range(- myRadius, myRadius), transform.position.y, transform.position.z + Random.Range(-myRadius, myRadius)), Quaternion.identity);
    }
}
