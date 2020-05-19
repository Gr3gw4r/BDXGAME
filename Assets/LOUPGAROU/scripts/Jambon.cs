using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jambon : MonoBehaviour
{
    public GameObject jambon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerHead"))
        {
            GetComponent<Timer>().Heal();
            Debug.Log("+10sec");
            Destroy(this.gameObject);
        }
    }
}
