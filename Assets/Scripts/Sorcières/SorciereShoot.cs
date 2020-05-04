using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SorciereShoot : MonoBehaviour
{
    public float Timer;
    private float timeActual;

    public SteamVR_Action_Boolean useAction = null;
    public SteamVR_Action_Boolean shieldKey = null;
    private GameObject bullet;
    private SteamVR_Behaviour_Pose pose;

    private float CD;
    private float CDActual = 0;
    private bool isReloading = false;

    private int shootStockActual;

    public GameObject shield;
    private bool isDefending = false;

    public Transform headVR;

    // Start is called before the first frame update
    void Start()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        bullet = GameManager_Sorciere.Instance.GetShoot();
        CD = GameManager_Sorciere.Instance.GetCD();

        shootStockActual = GameManager_Sorciere.Instance.GetBulletNumber();

        timeActual = Timer;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading == true)
        {
            CDActual += Time.deltaTime;
        }

        if (CDActual >= CD)
        {
            isReloading = false;
            CDActual = 0;
        }

        if ((useAction.GetLastStateDown(pose.inputSource)) && (isReloading == false) && (GameManager_Sorciere.Instance.GetDefending() == false))
        {
            shootStockActual = GameManager_Sorciere.Instance.GetBulletNumber();

            if (shootStockActual > 0)
            {
                 Shoot();
            }
        }

        if ((shieldKey.GetLastStateDown(pose.inputSource)) && (GameManager_Sorciere.Instance.GetDefending() == false))
        {
            shield.SetActive(true);
            isDefending = true;
            GameManager_Sorciere.Instance.SetDefending(true);
        }
        else if (shieldKey.GetLastStateUp(pose.inputSource))
        {
            shield.SetActive(false);
            isDefending = false;
            GameManager_Sorciere.Instance.SetDefending(false);
        }
    }

    public void Shoot()
    {
        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.transform.rotation = transform.rotation;
        GameManager_Sorciere.Instance.SetBulletNumber(-1);
        isReloading = true;
    }
}