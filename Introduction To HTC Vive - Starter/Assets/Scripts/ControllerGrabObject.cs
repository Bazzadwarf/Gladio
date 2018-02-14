using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerGrabObject : MonoBehaviour {

	private SteamVR_TrackedObject trackedObj;
	private GameObject collidingObject; 
	private GameObject objectInHand;
    public Transform cameraRigTransform;

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
        if (objectInHand.layer == 9)
        {
            switch(objectInHand.name)
            {
                case "sword":
                    objectInHand.transform.position = Controller.transform.pos - cameraRigTransform.position;
                    objectInHand.transform.rotation = Controller.transform.rot; // cameraRigTransform.rotation;
                    break;
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

	void Update () {
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
