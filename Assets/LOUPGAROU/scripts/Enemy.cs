using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector3 movement;
    public int scoreValue;
    public float life;
    private float lifeActual;

    private Animator myAnimator;

    private NavMeshAgent myNavMeshAgent;

    public bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        lifeActual = life;

        int myModelIndex = Random.Range(0, transform.childCount);

        transform.GetChild(myModelIndex).gameObject.SetActive(true);
        myAnimator = transform.GetChild(myModelIndex).gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            GameManager_LG2.Instance.AddScore(scoreValue);
            Destroy(this.gameObject);
        }

        if (myNavMeshAgent.isActiveAndEnabled == false && myNavMeshAgent.Warp(transform.position) == true)
        {
            myNavMeshAgent.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        moveCharacter();
    }

    void moveCharacter()
    {
        if (myNavMeshAgent.isActiveAndEnabled == true)
        {
            myNavMeshAgent.SetDestination(transform.position - GameManager_LG2.Instance.GetTarget().transform.position);
        }
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