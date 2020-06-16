using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    public float mySpeed;
    public Transform cameraTransform;

    private bool CanMove = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCanMove(bool newState)
    {
        CanMove = newState;
    }

    public void MoveForward()
    {
        if (CanMove == true)
        {
            Vector3 myDestination = cameraTransform.position + cameraTransform.forward;
            myDestination.y = transform.position.y;
            Vector3 myPosition = cameraTransform.position;
            myPosition.y = transform.position.y;
            Vector3 myTarget = transform.position + (myDestination - myPosition);
            transform.position = Vector3.MoveTowards(transform.position, myTarget, mySpeed * Time.deltaTime);
        }
    }
}