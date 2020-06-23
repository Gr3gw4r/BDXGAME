using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class TuToMessage
{
    public Sprite mySprite;
    public string myTittle;
    public string mySubtittle;
}

public class TutoScript : MonoBehaviour
{
    public static TutoScript Instance;

    public Image myPicture;
    public TextMeshProUGUI myTittle;
    public TextMeshProUGUI mySubTittle;

    private Animator myAnimator;

    public GameObject particles;

    public TuToMessage[] myMessages;
    private int indexTuto = 0;

    public Transform particlesSpawnPoint;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        ShowTuto();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowMe(Sprite newSprite, string newTittle, string newSubTittle)
    {
        Instantiate(particles, particlesSpawnPoint.position, Quaternion.identity);
        myPicture.sprite = newSprite;
        myTittle.text = newTittle;
        mySubTittle.text = newSubTittle;
    }

    public void StopMe()
    {
        myAnimator.SetBool("Hide", true);
    }

    public int GetTutoIndex()
    {
        return indexTuto;
    }

    public void ShowTuto()
    {
        if (indexTuto < myMessages.Length)
        {
            ShowMe(myMessages[indexTuto].mySprite, myMessages[indexTuto].myTittle, myMessages[indexTuto].mySubtittle);
        }

        indexTuto++;

        if (indexTuto > myMessages.Length)
        {
            StopMe();
        }
    }
}