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
    public string strSide;
    bool objectHeld = false;
    Vector3 velocity;

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
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
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
        // Check to see if it a weapon
        if (objectInHand.tag == "Weapon" || objectInHand.tag == "Shield")
        {
            // Gets all object from the wepaon
            List<GameObject> childGameObject = new List<GameObject>();
            GameObject snapPoint = null;

            for(int i = 0; i < objectInHand.transform.childCount; i++)
            {
                childGameObject.Add(objectInHand.transform.GetChild(i).gameObject);
                objectInHand.GetComponent<MeshCollider>().enabled = false;
            }

            // find the snap point to the object
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

                if (strSide == "left")
                {
                    objectInHand.transform.rotation = controllerSnapPoint.transform.rotation;
                    objectInHand.transform.rotation = snapPoint.transform.rotation;
                }
                else if (strSide == "right")
                {
                    objectInHand.transform.rotation = controllerSnapPoint.transform.rotation;
                     
                    objectInHand.transform.rotation = snapPoint.transform.rotation;
                    

                    float x = objectInHand.transform.rotation.x * (-1);

                    //Quaternion dlifhb = new Quaternion(x, controllerSnapPoint.transform.rotation.y, controllerSnapPoint.transform.rotation.z, controllerSnapPoint.transform.rotation.w);
                    //objectInHand.transform.rotation = dlifhb;
                    
                    //Quaternion rot = controllerSnapPoint.transform.rotation;
                    ////rot.x -= 180;
                    //rot.y -= 180;
                    ////rot.z -= 180;
                    //objectInHand.transform.rotation = rot;

                    //rot = snapPoint.transform.rotation;
                    //rot.x -= 180;
                    //rot.y -= 180;
                    //rot.z -= 180;
                    //objectInHand.transform.rotation = snapPoint.transform.rotation;
                }


                objectInHand.transform.position -= (snapPoint.transform.position - controllerSnapPoint.transform.position);

                snapPoint.transform.position = controllerSnapPoint.transform.position;
               

            }
        }


        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }
    
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 2000000;
        fx.breakTorque = 2000000;
        return fx;
    }
    private void ReleaseObject()
    {
        if (GetComponent<FixedJoint>())
        {
            objectInHand.GetComponent<MeshCollider>().enabled = true;
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
        objectInHand = null;
    }

    void Update()
    {
         velocity = Controller.velocity;
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
    public Vector3 GetVelocity()
    {
        return velocity;
    }
}
