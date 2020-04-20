using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tue : MonoBehaviour
{
    public GameObject Enemy;

    private void OnTriggerEnter(Collider other)
    {
        Destroy(Enemy);
    }
}
