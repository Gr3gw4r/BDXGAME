using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tue : MonoBehaviour
{
    public float Damage;

    public GameObject bloodParticles;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Instantiate(bloodParticles, transform.position, Quaternion.identity);
            other.GetComponent<Enemy>().Death();
        }

        if (other.CompareTag("Barrel"))
        {
            other.GetComponent<BarrelScript>().isBroke();
        }
    }
}
