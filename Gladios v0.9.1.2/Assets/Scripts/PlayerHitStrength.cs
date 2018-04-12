using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitStrength : MonoBehaviour {

    public float hitStrength;
    public float finalDamage;
    public float speed;
    public GameObject inHand;
    private Vector3 currentPos;
    private Vector3 lastPos;
    private float distance;

    // Use this for initialization
    void Start () {
        currentPos = inHand.transform.position;
	}

    // Update is called once per frame
    void Update()
    {
        //Work out distance travelled
        Vector3 tempVec;
        lastPos = currentPos;
        currentPos = inHand.transform.position;
        //Distance vector
        tempVec = new Vector3((lastPos.x - currentPos.x) * (lastPos.x - currentPos.x), 
                              (lastPos.y - currentPos.y) * (lastPos.y - currentPos.y), 
                              (lastPos.z - currentPos.z) * (lastPos.z - currentPos.z));
        //Actual distance
        distance = tempVec.x + tempVec.y + tempVec.z;
        //Speed of weapon
        speed = distance / Time.deltaTime;
        //Use speed as modifier
        finalDamage = hitStrength * speed;
        //Cap damage for no stupid numbers
        //This will still one-shot basic enemies
        if(finalDamage > 100) { finalDamage = 100; }
    }
}
