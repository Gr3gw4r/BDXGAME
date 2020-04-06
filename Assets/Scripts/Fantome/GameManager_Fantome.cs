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
    private GameObject objectToSpawn;
    public SpriteRenderer[] emptys;

    public float resetCD;
    private float CDActual = 0;

    private int score = 0;

    public static GameManager_Fantome Instance;

    public GameObject mGhost;
    public Transform parentSpawnPointM;
    public Transform parentSpawnPointF;

    public GameObject fGhost;
    public float CDFGhost;
    private float CDFGhostActual = 0;

    private bool MGhostInScene = false;

    public GameObject ghostCamera;

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
        CDFGhostActual += Time.deltaTime;

        if (CDActual >= resetCD)
        {
            SetEmptys();
        }

        if (CDFGhostActual >= CDFGhost)
        {
            SpawnFGhost();
            CDFGhostActual = 0;
        }
    }

    public void TakeObject(GameObject myObject)
    {

    }

    public void SetEmptys()
    {
        if (MGhostInScene == false)
        {
            int indexToSpawn = Random.Range(0, objectsToSpawnMGhost.Length);
            objectToSpawn = objectsToSpawnMGhost[indexToSpawn].myObject;

            for (int i = 0; i < emptys.Length; i++)
            {
                emptys[i].sprite = objectsToSpawnMGhost[indexToSpawn].myPictures[i];
            }

            CDActual = 0;
        }
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        Debug.Log(score);
    }

    public void SpawnMGhost()
    {
        if (MGhostInScene == false)
        {
            Instantiate(mGhost, parentSpawnPointM.GetChild(Random.Range(0, parentSpawnPointM.childCount)).transform.position, Quaternion.identity);
        }
    }

    public void SpawnFGhost()
    {
        if (MGhostInScene == false)
        {
            Instantiate(fGhost, parentSpawnPointF.GetChild(Random.Range(0, parentSpawnPointF.childCount)).transform.position, Quaternion.identity);
        }
    }

    public void setMGhostInScene(bool newMGhostInScene)
    {
        MGhostInScene = newMGhostInScene;
    }

    public GameObject GetObjectToSpawn()
    {
        return objectToSpawn;
    }

    public GameObject GetGhostCamera()
    {
        return ghostCamera;
    }
}