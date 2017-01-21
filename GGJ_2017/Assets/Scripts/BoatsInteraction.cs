using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatsInteraction : MonoBehaviour {

     public float SqrMinimumDistanceBetweenBoats;
     public float BoatDurability;
     public float DamageFromImpactPer1UnitOfSpeed;
     public float PushBackModifier;
     private Rigidbody rb;
     private Vector3 speed;
     private GameObject collidingObject;

     private float heightThreshold = 1f;
     public float floatingMultiplier;
     private GameObject water;

     private float boatHalfHeight;

     // Use this for initialization
     void Start () {
          rb = GetComponent<Rigidbody>();
          collidingObject = null;
          water = GameObject.FindWithTag("Water");
          boatHalfHeight = 5f;
     }
	
	// Update is called once per frame
	void Update () {
          speed = rb.velocity;
          simulateFloating();
	}

     void simulateFloating()
     {
          float zeroWaterLevel = 0f;

          float currentYLocation = transform.position.y,
          waterLevel = water.GetComponent<wave>().
          ProbingFunction(transform.position.x, transform.position.z, Time.time) - zeroWaterLevel;
               //GetWaterLevelFunction
               //getWaterLevelAt(new Vector2(transform.position.x, transform.position.z)) - zeroWaterLevel,
               

          print("CurrentYLocation = " + currentYLocation + "; waterLevel = " + waterLevel);

          if (currentYLocation < waterLevel)
          {
               rb.AddForce(0f, (waterLevel - currentYLocation + boatHalfHeight) * floatingMultiplier, 0f);
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
                    BoatsInteraction otherBoatScript = collidingObject.GetComponent<BoatsInteraction>();

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
