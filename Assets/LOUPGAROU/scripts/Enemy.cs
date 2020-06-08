using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform player;
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector3 movement;
    public int scoreValue;
    public float life;
    private float lifeActual;
    public GameObject shield;
    public GameObject bras;

    private NavMeshAgent myNavMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        lifeActual = life;

        player = GameManger_LG.Instance.GetTarget();
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
            GameManger_LG.Instance.AddScore(scoreValue);
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector3 direction)
    {
        //Vector3 myDirection = transform.position + (direction * moveSpeed * Time.deltaTime);
        myNavMeshAgent.SetDestination(GameManger_LG.Instance.GetPlayer().transform.position);
    }

    public void LooseLife(float addValue)
    {
        life -= addValue;
    }

    public void Death()
    {
        GameManger_LG.Instance.AddScore(scoreValue);
        Destroy(this.gameObject);
    }
}