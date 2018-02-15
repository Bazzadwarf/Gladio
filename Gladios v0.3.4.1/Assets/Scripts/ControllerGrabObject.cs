using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;
    private GameObject collidingObject;
    private GameObject objectInHand;
    public Transform cameraRigTransform;
    public GameObject controllerSnapPoint;

    bool objectHeld = false;


    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();

    }
    private void SetCollidingObject(Collider col)
    {
        // 1
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        // 2
        collidingObject = col.gameObject;
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }


    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // 3
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }
    private void GrabObject()
    {

        objectInHand = collidingObject;
        collidingObject = null;
        //Check to see if it a weapon
        if (objectInHand.tag == "Weapon")
        {
            //Gets all object from the wepaon
            List<GameObject> childGameObject = new List<GameObject>();
            GameObject snapPoint = null;

            for(int i = 0; i < objectInHand.transform.childCount; i++)
            {
                childGameObject.Add(objectInHand.transform.GetChild(i).gameObject);
            }

            //find the snap point to the object
            for (int i = 0; i < childGameObject.Count; i++)
            {
                if (childGameObject[i].tag == "SnapPoint")
                {
                    snapPoint = childGameObject[i];
                    break;
                }
            }

            // will make the snap point the same
            if (snapPoint != null)
            {
               
                objectInHand.transform.rotation = controllerSnapPoint.transform.rotation;
                objectInHand.transform.rotation = snapPoint.transform.rotation;
                
                objectInHand.transform.position -= (snapPoint.transform.position - controllerSnapPoint.transform.position);

                snapPoint.transform.position = controllerSnapPoint.transform.position;
               

            }
        }


        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    // 3
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }
    private void ReleaseObject()
    {
        // 1
        if (GetComponent<FixedJoint>())
        {
            // 2
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // 3
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
        // 4
        objectInHand = null;
    }

    void Update()
    {
        // 1
        if (Controller.GetHairTriggerDown())
        {

            if (collidingObject && !objectHeld)
            {
                GrabObject();
                objectHeld = true;
            }

            else if (objectInHand && objectHeld)
            {
                ReleaseObject();
                objectHeld = false;
            }
        }
    }
}
