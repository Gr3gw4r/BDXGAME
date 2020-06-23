using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    private Vector3 movement;
    public int scoreValue;

    private Animator myAnimator;

    private NavMeshAgent myNavMeshAgent;

    private bool isTriggered = false;
    public float distanceToTrigger;

    private Transform playerTransform;

    [SerializeField]
    private Transform[] navPoints;

    public Transform myModelsParent;

    public GameObject deathUI;
    public Transform deathUISpawnPoint;

    public float distanceForScore;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        int myModelIndex = Random.Range(0, myModelsParent.childCount);

        Debug.Log(myModelIndex);

        myModelsParent.GetChild(myModelIndex).gameObject.SetActive(true);
        myAnimator = myModelsParent.GetChild(myModelIndex).gameObject.GetComponent<Animator>();

        playerTransform = GameManager_LG2.Instance.GetTarget().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (myNavMeshAgent.isActiveAndEnabled == false && myNavMeshAgent.Warp(transform.position) == true)
        {
            myNavMeshAgent.enabled = true;
        }

        if (Vector3.Distance(transform.position, GameManager_LG2.Instance.GetTarget().transform.position) <= distanceToTrigger)
        {
            isTriggered = true;
        }

        if (Vector3.Distance(transform.position, GameManager_LG2.Instance.GetTarget().transform.position) > distanceToTrigger * 2)
        {
            isTriggered = false;
        }

        if (isTriggered == true)
        {
            myAnimator.SetFloat("Forward", 1);
        }
        else
        {
            myAnimator.SetFloat("Forward", 0);
        }
    }

    private void FixedUpdate()
    {
        RunFrom();
    }

    public void Death()
    {
        if (isDead == false)
        {
            isDead = true;

            GameManager_LG2.Instance.AddScore(scoreValue);

            var myUI = GameManager_LG2.Instance.GetAddScoresTextObject();

            myUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = GameManager_LG2.Instance.GetScore().ToString("0");
            myUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = ("x") + GameManager_LG2.Instance.GetMultiplier().ToString("0");

            GameManager_LG2.Instance.AddMultiplier();
            Destroy(gameObject);
        }
    }

    public void RunFrom()
    {
        if (myNavMeshAgent.isActiveAndEnabled == true && isTriggered == true)
        {
            Vector3 myDestination = transform.position - GameManager_LG2.Instance.GetTarget().transform.position;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(myDestination, out hit, Mathf.Infinity, NavMesh.AllAreas))
            {
                myDestination = hit.position;
            }

            myNavMeshAgent.SetDestination(transform.position + myDestination);
        }
    }
}