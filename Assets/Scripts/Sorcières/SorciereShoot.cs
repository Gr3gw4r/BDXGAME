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
    public SteamVR_Action_Boolean reloadKey = null;
    private GameObject bullet;
    private SteamVR_Behaviour_Pose pose;

    private float CD;
    private float CDActual = 0;
    private bool isReloading = false;

    private int shootStockActual;

    public GameObject shield;
    private bool isDefending = false;

    public GameObject reloadPattern;
    public GameObject reloadParticles;

    public Transform headVR;

    public bool canReload;
    public Transform reloadSignSpawnPoint;
    private GameObject reloadActualObject;

    public float CDReload;
    private float CDReloadActual;
    private bool isReloadingReload = false;

    // Start is called before the first frame update
    void Start()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        bullet = GameManager_Sorciere.Instance.GetShoot();
        CD = GameManager_Sorciere.Instance.GetCD();

        shootStockActual = GameManager_Sorciere.Instance.GetBulletNumber();

        timeActual = Timer;

        CDReloadActual = CDReload;
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

        if ((useAction.GetLastStateDown(pose.inputSource)) && (isReloading == false))
        {
            if ((GameManager_Sorciere.Instance.GetDefending() == false))
            {
                shootStockActual = GameManager_Sorciere.Instance.GetBulletNumber();

                if (shootStockActual > 0)
                {
                    Shoot();
                }
            }
        }

        if (isReloadingReload == true)
        {
            CDReloadActual -= Time.deltaTime;

            if (CDReloadActual <= 0)
            {
                isReloadingReload = false;
            }
        }

        if (canReload)
        {
            if ((reloadKey.GetStateDown(pose.inputSource)) && (isDefending == false))
            {
                if (isReloadingReload == false)
                {
                    reloadParticles.SetActive(true);
                    reloadActualObject = Instantiate(reloadPattern, reloadSignSpawnPoint.position, Quaternion.identity);
                    reloadActualObject.transform.LookAt(new Vector3(GameManager_Sorciere.Instance.GetPlayerHead().position.x, reloadActualObject.transform.position.y, GameManager_Sorciere.Instance.GetPlayerHead().position.z));
                    isReloadingReload = true;
                    CDReloadActual = CDReload;
                }
            }
        }

        if (reloadKey.GetLastStateUp(pose.inputSource))
        {
            reloadParticles.SetActive(false);

            if (reloadActualObject != null)
            {
                Destroy(reloadActualObject);
            }
        }

        if ((shieldKey.GetStateDown(pose.inputSource)) && (GameManager_Sorciere.Instance.GetDefending() == false))
        {
            if (TutoScript.Instance.GetTutoIndex() == 2)
            {
                TutoScript.Instance.ShowTuto();
            }

            shield.SetActive(true);
            isDefending = true;
            GameManager_Sorciere.Instance.SetDefending(true);
        }

        if (shieldKey.GetLastStateUp(pose.inputSource) && (isDefending == true))
        {
            shield.SetActive(false);
            GameManager_Sorciere.Instance.SetDefending(false);
            isDefending = false;
        }
    }

    public void Shoot()
    {
        if (TutoScript.Instance.GetTutoIndex() == 1)
        {
            TutoScript.Instance.ShowTuto();
        }

        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
        newBullet.transform.rotation = transform.rotation;
        GameManager_Sorciere.Instance.SetBulletNumber(-1);
        isReloading = true;
    }
}