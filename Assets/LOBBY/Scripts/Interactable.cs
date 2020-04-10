using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public enum gamemodes { Sorciere, Fantome, LoupGarou};

public class Interactable : MonoBehaviour
{
    public gamemodes myGamemode;
    public SteamVR_Action_Boolean grabAction = null;
    public SteamVR_Action_Boolean useAction = null;

    private SteamVR_Behaviour_Pose pose;
    private FixedJoint joint = null;

    private PropsInteractable currentInteractable = null;
    private List<PropsInteractable> contactInteractables = new List<PropsInteractable>();

    // Start is called before the first frame update
    void Start()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        joint = GetComponent<FixedJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (grabAction.GetStateDown(pose.inputSource))
        {
            Pickup();
        }

        if (grabAction.GetStateUp(pose.inputSource))
        {
            Drop();
        }

        if (currentInteractable != null)
        {
            Debug.Log(currentInteractable.gameObject.GetComponent<PhotoManager>() != null);
            if (useAction.GetStateDown(pose.inputSource) && currentInteractable.gameObject.GetComponent<PhotoManager>() != null)
            {
                currentInteractable.GetComponent<PhotoManager>().MakePhoto();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            contactInteractables.Add(other.gameObject.GetComponent<PropsInteractable>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        contactInteractables.Remove(other.gameObject.GetComponent<PropsInteractable>());
    }

    public void Pickup()
    {
        currentInteractable = GetNearestInteractable();

        if (!currentInteractable)
        {
            return;
        }

        if (currentInteractable.activeHand)
        {
            currentInteractable.activeHand.Drop();
        }

        currentInteractable.transform.position = transform.position;

        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        joint.connectedBody = targetBody;

        currentInteractable.activeHand = this;
    }

    void Drop()
    {
        if (!currentInteractable)
        {
            return;
        }

        Rigidbody targetBody = currentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = pose.GetVelocity();
        targetBody.angularVelocity = pose.GetAngularVelocity();

        joint.connectedBody = null;

        currentInteractable.activeHand = null;
        currentInteractable = null;
    }

    public PropsInteractable GetNearestInteractable()
    {
        PropsInteractable nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (PropsInteractable interactable in contactInteractables)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }

        return nearest;
    }
}
