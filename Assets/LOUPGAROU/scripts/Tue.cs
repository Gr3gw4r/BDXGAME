﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tue : MonoBehaviour
{
    public GameObject Enemy;
    public float Damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnnemySorciereScript>().LooseLife(Damage);
        }
    }


}
