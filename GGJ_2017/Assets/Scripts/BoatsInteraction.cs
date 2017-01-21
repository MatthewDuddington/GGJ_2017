using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatsInteraction : MonoBehaviour
{

     public float SqrMinimumDistanceBetweenBoats;
     public float BoatDurability;
     public float DamageFromImpactPer1UnitOfSpeed;
     public float PushBackModifier;
     private Rigidbody rb;
     private Vector3 speed;
     private GameObject collidingObject;

     public float WavePushForce;

     private float heightThreshold = 1f;
     public float floatingMultiplier;
     private wave water;

     public float boatHalfHeight;

     public Transform[] hullFloatingPoints;
     float zeroWaterLevel = 0f;

     private float xMax;
     private float xMin;
     private float zMax;
     private float zMin;

     // Use this for initialization
     void Start()
     {
          rb = GetComponent<Rigidbody>();
          collidingObject = null;
          water = GameObject.FindWithTag("Water").GetComponent<wave>();
          xMax = (water.gameObject.transform.localScale * water.xSize / 2).x;
          xMin = -xMax;

          zMax = (water.gameObject.transform.localScale * water.ySize / 2).z;
          zMin = -zMax;

          zeroWaterLevel = water.transform.position.y;
          print(xMax);

     }

     // Update is called once per frame
     void Update()
     {
          float x = transform.position.x,
               z = transform.position.z;
        
          if (x < xMax && x > xMin && z < zMax && z > zMin)
          {
               speed = rb.velocity;
               simulateFloating();

               addWaveForce();
          }
     }

     private void addWaveForce()
     {
          Vector3 forceAmount = WavePushForce * water.CalculateNormal(transform.position.x, transform.position.z, Time.time);
          Debug.DrawLine(transform.position, transform.position + forceAmount / 4);
          rb.AddForce(WavePushForce * water.CalculateNormal(transform.position.x, transform.position.z, Time.time));
     }

     void simulateFloating()
     {
          if (water)
          {
               //float[] waterLevels = new float[4];
               foreach (Transform point in hullFloatingPoints)
               {
                    float waterLevel = water.ProbingFunction(point.position.x, point.position.z, Time.time) - zeroWaterLevel;
                    float currentYLocation = point.position.y;
                    if (currentYLocation < waterLevel)
                    {
                         Vector3 forceAmount = new Vector3(0f, (waterLevel - currentYLocation) * floatingMultiplier + boatHalfHeight, 0f);
                         rb.AddForceAtPosition(forceAmount / 4, point.transform.position);
                         //Debug.DrawLine(point.transform.position, point.transform.position + forceAmount / 4);
                         //rb.AddForce(0f, (waterLevel - currentYLocation) * floatingMultiplier + boatHalfHeight, 0f);
                    }
               }


               //GetWaterLevelFunction
               //getWaterLevelAt(new Vector2(transform.position.x, transform.position.z)) - zeroWaterLevel,


               // print("CurrentYLocation = " + currentYLocation + "; waterLevel = " + waterLevel);


          }
     }

     void fourPointsFloating()
     {
          float currentYLocation = transform.position.y,
          waterLevel = water.ProbingFunction(transform.position.x, transform.position.z, Time.time)
               - zeroWaterLevel;
          //GetWaterLevelFunction
          //getWaterLevelAt(new Vector2(transform.position.x, transform.position.z)) - zeroWaterLevel,


          // print("CurrentYLocation = " + currentYLocation + "; waterLevel = " + waterLevel);

          if (currentYLocation < waterLevel)
          {
               rb.AddForce(0f, (waterLevel - currentYLocation) * floatingMultiplier + boatHalfHeight, 0f);
          }
     }

     private void OnCollisionEnter(Collision collision)
     {
          if (collision.gameObject.tag == "Player")
          {
               print("Collision!");
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
