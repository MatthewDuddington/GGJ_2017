using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatsCollision : MonoBehaviour {

     public float SqrMinimumDistanceBetweenBoats;
     public float BoatDurability;
     public float DamageFromImpactPer1UnitOfSpeed;
     
     private Rigidbody rb;
     private Vector3 speed;
     private GameObject collidingObject;

     // Use this for initialization
     void Start () {
          rb = GetComponent<Rigidbody>();
          collidingObject = null;
     }
	
	// Update is called once per frame
	void Update () {
          speed = rb.velocity;
	}

     private void OnCollisionEnter(Collision collision)
     {
          if (collision.gameObject.tag == "Player")
               if (!collidingObject)
          {
               collidingObject = collision.gameObject;
               BoatsCollision otherBoatScript = collidingObject.GetComponent<BoatsCollision>();

               Vector3 otherRBPreviousFrameSpeed = 2 * rb.velocity - speed;
               Vector3 boatsAxis = collision.rigidbody.transform.position - rb.transform.position;
               float damageToTheSecond = Vector3.Project(rb.velocity, boatsAxis).magnitude;
               float damageToTheFirst = Vector3.Project(otherRBPreviousFrameSpeed, boatsAxis).magnitude;

               BoatDurability -= damageToTheFirst * DamageFromImpactPer1UnitOfSpeed;
               
               otherBoatScript.BoatDurability -= damageToTheSecond* otherBoatScript.DamageFromImpactPer1UnitOfSpeed;
               print("Damage1 = " + damageToTheFirst + "; Damage2 = " + damageToTheSecond);
          }
          else
               {
                    if ((collidingObject.transform.position - transform.position).sqrMagnitude >= SqrMinimumDistanceBetweenBoats)
                         collidingObject = null;
               }
     }
}
