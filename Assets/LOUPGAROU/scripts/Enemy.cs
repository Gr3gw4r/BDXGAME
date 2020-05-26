using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector3 movement;
    public int Score;
    public float life;
    private float lifeActual;
    public GameObject shield;
    public GameObject bras;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        lifeActual = life;

        player = Timer.Instance.GetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;
        if(life <= 10)
        {
            Destroy(shield.gameObject);
        }
        if (life <= 5)
        {
            Destroy(bras.gameObject);
        }
        if (life <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector3 direction)
    {
        Vector3 myDirection = transform.position + (direction * moveSpeed * Time.deltaTime);
        rb.MovePosition(new Vector3(myDirection.x, transform.position.y, myDirection.z));
    }

    public void LooseLife(float addValue)
    {
        life -= addValue;
    }
}