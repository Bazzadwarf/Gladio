using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour {

    public float maxHealth;
    public Slider healthBar;

    float currHealth;
    GameObject hitObject;
    bool inBox = false;

    // Use this for initialization
    void Start () {
        currHealth = maxHealth;
        RefreshHealthBar();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("test");
        hitObject = col.gameObject;
        if (hitObject.tag == "EnemyWeapon" && !inBox)
        {
            Debug.Log("hit " + hitObject.GetComponent<EnemyHitStrength>().hitStrength);
            currHealth -= hitObject.GetComponent<EnemyHitStrength>().hitStrength;
            inBox = true;
            RefreshHealthBar();
            if (currHealth <= 0)
            {
                Debug.Log("Dead");
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        Debug.Log("test2");
        if (hitObject == col.gameObject)
        {
            inBox = false;
        }
    }

    void RefreshHealthBar()
    {
        healthBar.value = (currHealth / maxHealth);
    }
}
