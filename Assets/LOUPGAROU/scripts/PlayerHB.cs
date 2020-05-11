using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHB : MonoBehaviour
{
    public float health = 100f;

    public Image healthbar;

    public void TakeDammage(float amount)
    {
        health -= amount;

        healthbar.fillAmount = health / 100;

        if(health <= 0)
        {
            Debug.Log("la mort");
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
}
