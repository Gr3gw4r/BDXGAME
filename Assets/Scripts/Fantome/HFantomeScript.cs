using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HFantomeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager_Fantome.Instance.setMGhostInScene(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameManager_Fantome.Instance.setMGhostInScene(false);
        GameManager_Fantome.Instance.SetEmptys();
    }
}
