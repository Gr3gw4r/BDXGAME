using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SorciereController : MonoBehaviour
{
    public SteamVR_Action_Boolean SpellCastAction = null;
    private SteamVR_Behaviour_Pose pose;

    private GameObject shoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (SpellCastAction.GetStateDown(pose.inputSource))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var newShoot = Instantiate(shoot, transform.position, Quaternion.identity);
        newShoot.transform.LookAt(transform.forward);
    }

    private void Spellcasting()
    {

    }
}
