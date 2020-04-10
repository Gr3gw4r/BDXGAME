using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyShootSorciere : MonoBehaviour
{
    private float myDamage = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDamage(float newDamage)
    {
        myDamage = newDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Astral"))
        {
            GameManager_Sorciere.Instance.LooseTime(myDamage);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            Destroy(gameObject);
        }
    }
}
