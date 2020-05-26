using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Area { Bonus, Fight, Empty}

public class TPPointScript : MonoBehaviour
{
    public Area MyArea = Area.Empty; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMyArea(Area newArea)
    {
        MyArea = newArea;

        Debug.Log(MyArea);
    }
}
