using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadSignScript : MonoBehaviour
{
    //public GameObject myShoot;
    public float maxTimeToReachNextPoint;
    private float maxTimeToReachNextPointActual = 0;

    private int actualPosition = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (actualPosition > 0)
        {
            maxTimeToReachNextPointActual += Time.deltaTime;

            if (maxTimeToReachNextPointActual > maxTimeToReachNextPoint)
            {
                ResetPoints();
                Debug.Log("plus le time");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ReloadPoint"))
        {
            int newPosition = other.GetComponent<TriggerPointReloadScript>().GetMyNumber();

            if (newPosition == actualPosition + 1)
            {
                actualPosition += 1;
                maxTimeToReachNextPointActual = 0;

                Debug.Log(actualPosition);

                if (actualPosition >= 5)
                {
                    ResetPoints();
                }
            }
            else
            {
                ResetPoints();
                Debug.Log(actualPosition);
                Debug.Log("mauvais point");
            }
        }
    }

    private void ResetPoints()
    {
        actualPosition = 0;
        maxTimeToReachNextPointActual = 0;
        Debug.Log("pas bien ouej");
    }
}