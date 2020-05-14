using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerPointReloadScript : MonoBehaviour
{
    private int myNumber = 0;

    public TextMeshProUGUI myNumberText;

    // Start is called before the first frame update
    void Start()
    {
        myNumberText.text = myNumber.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMyNumber(int newNumber)
    {
        myNumber = newNumber;
    }

    public int GetMyNumber()
    {
        return myNumber;
    }
}