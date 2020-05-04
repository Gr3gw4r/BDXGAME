using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float myDamage;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + transform.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SorciereEnnemies"))
        {
            other.GetComponent<EnnemySorciereScript>().LooseLife(myDamage);
            Destroy(this.gameObject);
        }
    }
}
