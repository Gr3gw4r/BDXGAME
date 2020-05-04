using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FantomeScript : MonoBehaviour
{
    public int scoreValue;
    public float lifeTime;
    private float lifeTimeActual;

    public GameObject deathParticles;

    // Start is called before the first frame update
    void Start()
    {
        lifeTimeActual = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimeActual -= Time.deltaTime;

        if (lifeTimeActual <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public int GetMyScore()
    {
        return scoreValue;
    }

    public void Respawn()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}