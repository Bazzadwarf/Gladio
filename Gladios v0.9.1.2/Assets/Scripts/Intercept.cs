using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intercept : MonoBehaviour {

    // Use this for initialization
    public Animator animator;
    public int intTime = 0;
   // public string strAnimationName;
    
    void Start () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        GameObject colideObject =  col.gameObject;
        if(intTime > 100 && (colideObject.tag == "Weapon" || colideObject.tag == "Shield"))
        {
            animator.SetTrigger("Reverse");
            //animator.SetBool("Reverse", true);
            //intTime = 0;
        }
    }


    // Update is called once per frame
    void Update()
    {
        intTime++;
        if (intTime > 50)
        {
            //animator.SetTrigger("Reverse")
            //animator.SetBool("Reverse", false);
        }
        
    }
}
