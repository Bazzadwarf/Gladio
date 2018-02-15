using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjects : MonoBehaviour
{

    // Use this for initialization
    
    private GameObject hitObject;
    void Start()
    {
        
    }

     void OnTriggerEnter(Collider col)
    {
        hitObject = col.gameObject;
        if (hitObject.layer == 9)
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        
       
    }
}
