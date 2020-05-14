using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reloadScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager_Sorciere.Instance.SetReloadObject(this.gameObject);

        for (int i = 1; i < transform.childCount + 1; i++)
        {
            int k = Random.Range(0, transform.childCount - 1);

            while (transform.GetChild(k).GetComponent<TriggerPointReloadScript>().GetMyNumber() != 0)
            {
                k ++;

                if (k >= transform.childCount)
                {
                    k = 0;
                }
            }

            transform.GetChild(k).GetComponent<TriggerPointReloadScript>().SetMyNumber(i);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}