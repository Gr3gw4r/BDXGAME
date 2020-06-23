using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyShootSorciere : MonoBehaviour
{
    private float myDamage;

    public GameObject destroyedParticles;

    public GameObject damagedParticles;

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
        if (other.gameObject.CompareTag("PlayerDamaged"))
        {
            GameManager_Sorciere.Instance.LooseTime(myDamage);
            Instantiate(damagedParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Shield"))
        {
            AudioManager.Instance.PlaySound("Block");
            Instantiate(destroyedParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
