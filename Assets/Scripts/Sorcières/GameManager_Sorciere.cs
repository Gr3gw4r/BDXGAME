using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum spells { Attack, Defend, TimeSlow}

public class GameManager_Sorciere : MonoBehaviour
{
    public static GameManager_Sorciere Instance;

    public GameObject shoot;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
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

    public GameObject GetShoot()
    {
        return shoot;
    }
}
