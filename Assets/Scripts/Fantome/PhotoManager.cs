using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoManager : MonoBehaviour
{
    public float maxDistance;

    public Camera photo;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MakePhoto()
    {
        GameObject[] ghosts = GameObject.FindGameObjectsWithTag("Ghost");

        foreach (GameObject myGhosts in ghosts)
        {
            RaycastHit hit;

            var direction = myGhosts.transform.position - transform.position;

            if (Physics.Raycast(transform.position, direction, out hit, maxDistance))
            {
                if (hit.transform.gameObject.CompareTag("Ghost"))
                {
                    Vector3 screenPoint = photo.WorldToViewportPoint(myGhosts.transform.position);
                    bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

                    if (onScreen)
                    {
                        hit.transform.gameObject.GetComponent<FantomeScript>().Respawn();
                        int addScore = hit.transform.gameObject.GetComponent<FantomeScript>().GetMyScore();
                        GameManager_Fantome.Instance.AddScore(addScore);
                    }
                }
            }
        }
    }
}
