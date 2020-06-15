using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectColliderScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LGWall"))
        {
            PlayerMovement.Instance.SetCanMove(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LGWall"))
        {
            PlayerMovement.Instance.SetCanMove(true);
        }
    }
}
