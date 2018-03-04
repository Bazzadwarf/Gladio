using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    public float health;
    GameObject hitObject;
    bool inBox = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        hitObject = col.gameObject;
        if (hitObject.tag == "EnomeyWeapon" && !inBox)
        {

           

            //Debug.Log((int)(velocity.magnitude * 10));

            health -= hitObject.GetComponent<HitStength>().hitStrength;
            inBox = true;
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
           
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (hitObject == col.gameObject)
        {
            inBox = false;
        }
    }
}
