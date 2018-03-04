using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float targetDistance;
    float setAttackDistance;
    public float enemyLookDistance;
    public float attackDistance;
    public float enemyMovementSpeed;
    public float damping;
    public float health;
    public GameObject thisGameObject;
    public Animator animator;

    public float startAngleX;
    public float startAngleY;
    public float startAngleZ;

    bool inBox = false;
    Vector3 velocity;
    Vector3 angularVelocity;
    public Transform viveTarget;

  
    private GameObject hitObject;
    bool moveTowardsPlayer = true;
    int moveBackTime;
    int moveBackCount = 0;

    Vector3 lastLocation;
    Quaternion lastRotaction;
    // Use this for initialization
    void Start()
    { 
        lastLocation = thisGameObject.transform.position;
        lastRotaction = thisGameObject.transform.rotation;
        setAttackDistance = attackDistance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetDistance = Vector3.Distance(viveTarget.position, transform.position);

        if (!moveTowardsPlayer)
        {
         //   myRender.material.color = Color.black;
            MoveAwayFromPlayer();
            moveBackCount++;
            if (moveBackCount == moveBackTime)
            {
                moveBackCount = 0;
                moveTowardsPlayer = true;
            }
        }
        else if (targetDistance > attackDistance)
        {
            
           // myRender.material.color = Color.yellow;
            MoveAtPlayer();
            attackDistance = setAttackDistance;

      
        }
        else if (targetDistance < attackDistance)
        {
            Attack();
            attackDistance += (float)0.25;
            
           // myRender.material.color = Color.red;
        }
       
    }

    void MoveAtPlayer()
    {
        animator.SetBool("Moving", true);
        Vector3 dir = viveTarget.position - lastLocation;
        dir.y = 0;
        lastRotaction = Quaternion.Slerp(lastRotaction, Quaternion.LookRotation(dir), Time.deltaTime * damping);
         thisGameObject.transform.rotation = lastRotaction;
       
        //transform.Translate(0, 0, enemyMovementSpeed);
        lastLocation = thisGameObject.transform.position;
        thisGameObject.transform.Rotate(startAngleX, startAngleY, startAngleZ);
       
     


    }
    void MoveAwayFromPlayer()
    {
        animator.SetBool("Moving", false);
        Vector3 dir = viveTarget.position - lastLocation;
        dir.y = 0;
        lastRotaction = Quaternion.Slerp(lastRotaction, Quaternion.LookRotation(dir), Time.deltaTime * damping);
        thisGameObject.transform.rotation = lastRotaction;

        thisGameObject.transform.Translate(0, 0, (float)-0.02);
        lastLocation = thisGameObject.transform.position;
        thisGameObject.transform.Rotate(startAngleX, startAngleY, startAngleZ);
    }

    void Attack()
    {
       int intAttackType =  Random.Range(0, 4);
        animator.SetInteger("AttackType", intAttackType);
        animator.SetBool("Moving", false);
        // rb.AddForce(transform.forward * enemyMovementSpeed);
    }

    void OnTriggerEnter(Collider col)
    {

        hitObject = col.gameObject;
        if (hitObject.tag == "Weapon" && !inBox)
        {
          
            velocity = hitObject.GetComponent<Rigidbody>().velocity;

            Debug.Log((int)(velocity.magnitude * 10));

            health -= hitObject.GetComponent<HitStength>().hitStrength;
            inBox = true;
            if (health <= 0)
            {
                Destroy(thisGameObject);
            }
            moveBackTime = 15;
            moveTowardsPlayer = false;
        }

        else if (hitObject.tag == "Shield" && !inBox)
        {
            inBox = true;
            moveBackTime = 60;
            moveTowardsPlayer = false;

        }
    }
    void OnTriggerExit(Collider col)
    {
        if (hitObject == col.gameObject)
        {
            inBox = false;
        }
    }

    void Update()
    {


    }
}
