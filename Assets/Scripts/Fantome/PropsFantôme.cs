using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsFantôme : MonoBehaviour
{
    private bool isTake = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if ((GetComponent<PropsInteractable>().activeHand != null) && (isTake == false))
        {
            if (this.gameObject == GameManager_Fantome.Instance.GetObjectToSpawn())
            {
                GameManager_Fantome.Instance.SpawnMGhost();
            }
            else
            {
                GameManager_Fantome.Instance.SetEmptys();
            }

            isTake = true;
        }

        if (GetComponent<PropsInteractable>().activeHand == null)
        {
            isTake = false;
        }*/
    }
}
