using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intercept : MonoBehaviour {

    // Use this for initialization
    public Animator animator;
    public int intTime = 0;

    public AudioClip shield1;
    public AudioClip shield2;
    public AudioClip shield3;
    public AudioClip sword1;
    public AudioClip sword2;
    public AudioClip sword3;
    public AudioClip sword4;
    public AudioClip sword5;
    public AudioClip sword6;
    public AudioClip sword7;

    AudioClip[] shield = new AudioClip[3];
    AudioClip[] sword = new AudioClip[7];
    void Start ()
    {
        shield = new AudioClip[3] { shield1, shield2, shield3 };
        sword = new AudioClip[7] { sword1, sword2, sword3, sword4, sword5, sword6, sword7 };
        // GetComponent<AudioSource>().clip = damaged;
        // GetComponent<AudioSource>().playOnAwake = false;
    }
    void OnTriggerEnter(Collider col)
    {
        GameObject collideObject =  col.gameObject;
        if(intTime > 100 && (collideObject.tag == "Weapon" || collideObject.tag == "Shield"))
        {
            if(collideObject.tag == "Weapon")
            {
                int intSound = Random.Range(0, 7);
               
                AudioSource.PlayClipAtPoint(sword[intSound], transform.position);
            }
            else if(collideObject.tag == "Shield")
            {
                int intSound = Random.Range(0, 3);

                AudioSource.PlayClipAtPoint(shield[intSound], transform.position);
            }
            Debug.Log("Test1234");
            animator.SetTrigger("Reverse");
           
        }
    }


    // Update is called once per frame
    void Update()
    {
        intTime++;
    }
}
