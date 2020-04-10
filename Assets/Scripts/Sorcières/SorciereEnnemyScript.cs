using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorciereEnnemyScript : MonoBehaviour
{
    public Transform spawnBulletPoint;
    public GameObject bullet;
    public float damage;

    public float CDToShoot;
    private float CDToShootActual = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CDToShootActual += Time.deltaTime;

        if (CDToShootActual >= CDToShoot)
        {
            Shoot();
            CDToShootActual = 0;
        }
    }
    public void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, spawnBulletPoint.position, Quaternion.identity);
        newBullet.transform.LookAt(GameManager_Sorciere.Instance.GetAstral().transform.position);
        newBullet.GetComponent<EnnemyShootSorciere>().SetDamage(damage);
    }
}