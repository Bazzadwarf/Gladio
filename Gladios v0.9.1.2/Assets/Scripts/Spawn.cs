using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {

    public int intMaxTime;
    public int intMinTime;
    int intWaittime;
    int intCount = 0;
    public GameObject Enemy1;
    //public GameObject knight;
    public Transform viveTarget;
    void Start ()
    {     
        intWaittime = Random.Range(intMinTime, intMaxTime);
        
        Enemy1.GetComponentInChildren<EnemyController>().viveTarget = viveTarget;
       // knight.GetComponent<EnemyController>().viveTarget = viveTarget;

	}
	
	// Update is called once per frame
	void Update ()
    {
        intCount++;
     if (intCount > intWaittime)
        {
            int intX = 0;
            int intZ = 0;
            int intDistance = 20;
            int intSide = Random.Range(0, 4);
            switch(intSide)
            {
                case 0:
                    intX = -intDistance;
                    intZ = Random.Range(-intDistance, intDistance);
                    break;
                case 1:
                    intX = intDistance;
                    intZ = Random.Range(-intDistance, intDistance);
                    break;
                case 2:
                    intX = Random.Range(-intDistance, intDistance);
                    intZ = intDistance;
                    break;
                case 3:
                    intX = Random.Range(-intDistance, intDistance);
                    intZ = -intDistance;
                    break;
              
            }
            Vector3 spawnLoacation;
            
            
                spawnLoacation = new Vector3(intX, Enemy1.transform.position.y, intZ);
                Instantiate(Enemy1, spawnLoacation, Enemy1.transform.rotation);
            
            

           intWaittime = Random.Range(intMinTime, intMaxTime);
            intCount = 0;
        }
	}
}
