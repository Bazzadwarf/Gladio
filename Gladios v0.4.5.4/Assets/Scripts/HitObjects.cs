using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjects : MonoBehaviour
{
    public float health;
    bool inBox = false;
    Vector3 velocity;
    Vector3 angularVelocity;

    private GameObject hitObject;
    void Start()
    {

    }

    void OnTriggerEnter(Collider col)
    {

        hitObject = col.gameObject;
        if (hitObject.tag == "Weapon" && !inBox)
        {
            //SteamVR_Controller.Device Controller;
            //angularVelocity = hitObject.GetComponent<Rigidbody>().angularVelocity;
            velocity = hitObject.GetComponent<Rigidbody>().velocity;
            //velocity = hitObject.GetComponentInParent<ControllerGrabObject>().GetVelocity();
            //float normalized = velocity.magnitude;
            // velocity = Controller.velocity;
            Debug.Log((int)(velocity.magnitude * 10));
            health -= hitObject.GetComponent<HitStength>().hitStrength;
            inBox = true;
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        else if (hitObject.tag == "Shield" && !inBox)
        {
            inBox = true;
            Vector3 vec = new Vector3( 0, 0,-2);
            Debug.Log("Vec" + vec.ToString());
            transform.Translate(vec);
            Debug.Log("transform" + transform.position.ToString());
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(hitObject == col.gameObject)
        {
            inBox = false;
        }
    }

    void Update()
    {
        
       
    }
}
