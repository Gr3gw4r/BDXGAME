using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MGhostObject
{
    public GameObject myObject;
    public Sprite[] myPictures;
    private bool isShowing = false;
}

public class GameManager_Fantome : MonoBehaviour
{
    public MGhostObject[] objectsToSpawnMGhost;
    public SpriteRenderer[] emptys;

    public float resetCD;
    private float CDActual = 0;

    private int score = 0;

    public static GameManager_Fantome Instance;

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
        SetEmptys();
    }

    // Update is called once per frame
    void Update()
    {
        CDActual += Time.deltaTime;

        Debug.Log(score);

        if (CDActual >= resetCD)
        {
            SetEmptys();
        }
    }

    public void SetEmptys()
    {
        int objectToSpawn = Random.Range(0, objectsToSpawnMGhost.Length);

        for (int i = 0; i < emptys.Length; i ++)
        {
            emptys[i].sprite = objectsToSpawnMGhost[objectToSpawn].myPictures[i];
        }

        CDActual = 0;
    }

    public void AddScore(int newScore)
    {
        score += newScore;

    }
}