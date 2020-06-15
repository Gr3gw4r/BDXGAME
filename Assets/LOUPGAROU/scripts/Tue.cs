using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tue : MonoBehaviour
{
    public float Damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("salut");
            other.GetComponent<Enemy>().Death();
        }

        if (other.CompareTag("Barrel"))
        {
            other.GetComponent<BarrelScript>().isBroke();
        }
    }
}
