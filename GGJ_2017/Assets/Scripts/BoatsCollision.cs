using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatsCollision : MonoBehaviour {

     public float SqrMinimumDistanceBetweenBoats;
     public float BoatDurability;
     public float DamageFromImpactPer1UnitOfSpeed;
     public float PushBackModifier;
     private Rigidbody rb;
     private Vector3 speed;
     private GameObject collidingObject;

     private float heightThreshold = 1f;
     public float floatingMultiplier;
     // Use this for initialization
     void Start () {
          rb = GetComponent<Rigidbody>();
          collidingObject = null;
     }
	
	// Update is called once per frame
	void Update () {
          speed = rb.velocity;
          simulateFloating();
	}

     void simulateFloating()
     {
          float currentHeight = transform.position.y;
          if (currentHeight < heightThreshold)
          {
               rb.AddForce(0f, (heightThreshold - currentHeight) * floatingMultiplier, 0f);
          }
     }

     private void OnCollisionEnter(Collision collision)
     {
          if (collision.gameObject.tag == "Player")
          {
               if (collidingObject)
               {
                    if ((collidingObject.transform.position - transform.position).sqrMagnitude >= SqrMinimumDistanceBetweenBoats)
                         collidingObject = null;
               }
               if (!collidingObject)
               {
                    collidingObject = collision.gameObject;
                    BoatsCollision otherBoatScript = collidingObject.GetComponent<BoatsCollision>();

                    Vector3 otherRBPreviousFrameSpeed = 2 * rb.velocity - speed;
                    Vector3 boatsAxis = collision.rigidbody.transform.position - rb.transform.position;
                    float damageToTheSecond = Vector3.Project(rb.velocity, boatsAxis).magnitude;
                    float damageToTheFirst = Vector3.Project(otherRBPreviousFrameSpeed, boatsAxis).magnitude;

                    BoatDurability -= damageToTheFirst * DamageFromImpactPer1UnitOfSpeed;

                    otherBoatScript.BoatDurability -= damageToTheSecond * otherBoatScript.DamageFromImpactPer1UnitOfSpeed;
                    print("Damage1 = " + damageToTheFirst + "; Damage2 = " + damageToTheSecond);

                    print("Normal = " + collision.contacts[0].normal);
                    rb.AddForceAtPosition(collision.impulse.magnitude * PushBackModifier * collision.contacts[0].normal, collision.contacts[0].point);
                    collision.rigidbody.AddForceAtPosition(collision.impulse.magnitude * PushBackModifier * -collision.contacts[0].normal, collision.contacts[0].point);
               }
          }
     }
}
