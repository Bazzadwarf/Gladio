using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitStrength : MonoBehaviour {

    public float hitStrength;
    private float strength;
    public Animator animator;

    void Start ()
    {
        strength = hitStrength;
    }

	// Update is called once per frame
	void Update ()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetCurrentAnimatorStateInfo(0).IsName("Run"))
        {
            hitStrength = 0;
        }
        else
        {
            hitStrength = strength;
        }
	}
}
