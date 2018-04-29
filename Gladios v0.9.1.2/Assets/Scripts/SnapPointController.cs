using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapPointController : MonoBehaviour {

    public GameObject parent;
    public GameObject thisObject;


	// Use this for initialization
	void Start ()
    {    
            parent.transform.SetParent(thisObject.transform);

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
