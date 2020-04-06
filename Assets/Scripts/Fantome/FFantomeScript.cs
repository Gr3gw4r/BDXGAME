using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFantomeScript : MonoBehaviour
{
    public float maxDistanceToBeSee;
    private GameObject ghostCamera;
    private float direction;
    public float speed;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        ghostCamera = GameManager_Fantome.Instance.GetGhostCamera();
        direction = Random.Range(0, 360);
        transform.rotation = Quaternion.Euler(0, direction, 0);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.MovePosition(transform.position + (transform.forward * speed * Time.deltaTime));

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward * speed * Time.deltaTime, 1f))
        {
            direction = Random.Range(0, 360);
            transform.rotation = Quaternion.Euler(0, direction, 0);
        }
    }
}
