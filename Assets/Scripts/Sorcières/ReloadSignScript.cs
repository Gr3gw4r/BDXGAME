using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadSignScript : MonoBehaviour
{
    //public GameObject myShoot;
    public float maxTimeToReachNextPoint;
    private float maxTimeToReachNextPointActual = 0;

    private int actualPosition = 0;

    public GameObject reloadParticles;

    public float CDReload;
    private float CDReloadActual;
    private bool isReloadingReload = false;

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
                GameManager_Sorciere.Instance.DestroyReloadObject();
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
                Instantiate(reloadParticles, transform.position, Quaternion.identity);

                actualPosition += 1;
                maxTimeToReachNextPointActual = 0;

                Instantiate(reloadParticles, transform.position, Quaternion.identity);

                Destroy(other.gameObject);

                if (actualPosition >= 5)
                {
                    if (TutoScript.Instance.GetTutoIndex() == 3)
                    {
                        TutoScript.Instance.ShowTuto();
                    }

                    GameManager_Sorciere.Instance.Reload();
                    ResetPoints();
                }
            }
            else
            {
                GameManager_Sorciere.Instance.DestroyReloadObject();
                ResetPoints();
            }
        }
    }

    private void ResetPoints()
    {
        actualPosition = 0;
        maxTimeToReachNextPointActual = 0;
    }
}