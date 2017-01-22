using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoatInteraction : MonoBehaviour
{
     public float SqrMinimumDistanceBetweenBoats;
     public float BoatDurability;
     public float DamageFromImpactPer1UnitOfSpeed;
     public float PushBackModifier;
     private Rigidbody rb;
     private Vector3 speed;
     private GameObject collidingObject;

     public float WavePushForce;

     private wave water;

     private float xMax;
     private float xMin;
     private float zMax;
     private float zMin;

     public Material normalHullMaterial;
     public Material normalDarkHullMaterial;
     public Material normalWoodMaterial;
     public Material normalEdgeMaterial;
     public Material normalSailMaterial;
     public Material ironMaterial;
     public MeshRenderer hull;
     public MeshRenderer crowsnest;
	 public MeshRenderer mast;
	 public MeshRenderer sailLower;
	 public MeshRenderer sailUpper;
	 public MeshRenderer sail;

     public int CoinTotal = 100;
     public int numberOfCoinsToDropWhenHit = 10;
     public float CoinToWeightRatio;

     public bool isIronclad_;
     private static bool collisionOccured;

     public GameObject spawningCoinPrefab;

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
         // print(xMax);

          collisionOccured = false;
     }

     // Update is called once per frame
     void Update()
     {
          float x = transform.position.x,
               z = transform.position.z;

          speed = rb.velocity;
          if (x < xMax && x > xMin && z < zMax && z > zMin)
          {
               addWaveForce();
          }
          else
               print("off the map");
     }

     private void addWaveForce()
     {
          Vector3 forceAmount = WavePushForce * water.CalculateNormal(transform.position.x, transform.position.z, Time.time);
          Debug.DrawLine(transform.position, transform.position + forceAmount / 4);
          rb.AddForce(WavePushForce * water.CalculateNormal(transform.position.x, transform.position.z, Time.time));
     }

     void OnCollisionEnter(Collision collision)
     {
          string tag = collision.gameObject.tag;
          if (tag == "Player")
          {
               if (collidingObject)
               {
                    if ((collidingObject.transform.position - transform.position).sqrMagnitude >= SqrMinimumDistanceBetweenBoats)
                         collidingObject = null;
               }
               if (!collidingObject)
               {
                    if (collisionOccured)
                         collisionOccured = false;
                    else
                    {
                         collisionOccured = true;
                         collidingObject = collision.gameObject;
                         BoatInteraction otherBoatScript = collidingObject.GetComponent<BoatInteraction>();

                         float actualPushBackModifier = PushBackModifier;
                         //ironclad thing
                         if (isIronclad_)
                         {
                              if (otherBoatScript.isIronclad_)
                              {
                                   otherBoatScript.isIronclad_ = false;
							       otherBoatScript.resetShipMaterials();
                                   //modify push back value here
                                   actualPushBackModifier *= 2;
                                   //sound of clang
                              }
                              else
                              {
                                   otherBoatScript.CoinTotal -= numberOfCoinsToDropWhenHit;
                                   for (int i = numberOfCoinsToDropWhenHit; i > 0; i--)
                                   {                                        
                                        GameObject coin = Instantiate(spawningCoinPrefab);
                                        coin.transform.position = collidingObject.transform.position + new Vector3(0, 15, 0);
                                        //coin.transform.localScale = new Vector3(1.5,1.5,1.5);


                                        //coin.transform.rotation = Random.rotation;
                                        coin.transform.Rotate(Vector3.up, Random.Range(0f, 359.999f));
                                        coin.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(1000 + Random.Range(0, 1500), 1500 + Random.Range(0, 3000), 0));  //TODO define propper force
                                   }
                              }
                              isIronclad_ = false;
							  resetShipMaterials();
                         }

                         //damage thing
                         Vector3 otherRBPreviousFrameSpeed = 2 * rb.velocity - speed;
                         Vector3 boatsAxis = collision.rigidbody.transform.position - rb.transform.position;
                         float damageToTheSecond = Vector3.Project(rb.velocity, boatsAxis).magnitude;
                         float damageToTheFirst = Vector3.Project(otherRBPreviousFrameSpeed, boatsAxis).magnitude;

                         BoatDurability -= damageToTheFirst * DamageFromImpactPer1UnitOfSpeed;

                         otherBoatScript.BoatDurability -= damageToTheSecond * otherBoatScript.DamageFromImpactPer1UnitOfSpeed;

                         //pushback thing
                         rb.AddForceAtPosition(collision.impulse.magnitude * actualPushBackModifier * collision.contacts[0].normal, collision.contacts[0].point);
                         collision.rigidbody.AddForceAtPosition(collision.impulse.magnitude * actualPushBackModifier * -collision.contacts[0].normal, collision.contacts[0].point);
                    }
               }
          }
          else if (tag == "Coin")
          {
               print("is a coin");
               int receivedAmount = collision.gameObject.GetComponent<Coin>().value;
               collision.gameObject.SetActive(false);
               rb.mass += CoinToWeightRatio * receivedAmount;
               CoinTotal += receivedAmount;
          }
          else if (tag == "Iron")
          {
               print("is some iron");
               collision.gameObject.SetActive(false);
               Destroy(collision.gameObject);

               isIronclad_ = true;
               foreach(Material mat in hull.materials) {
               		mat = ironMaterial;
               }
          }
     }

     public void PickupCoins(int numberOfCoins)
     {
          CoinTotal += numberOfCoins;
          // TODO Update UI
     }

     public float Weight()
     {
          return CoinTotal * CoinToWeightRatio;
     }

     public bool IsIronclad()
     {
          return isIronclad_;
     }

     public void resetShipMaterials() {
     	hull.materials[0] = normalDarkHullMaterial;
     	hull.materials[1] = normalHullMaterial;
     	hull.materials[2] = normalEdgeMaterial;
		crowsnest.material = normalWoodMaterial;
		mast.material = normalWoodMaterial;
		sailLower.material = normalWoodMaterial;
		sailUpper.material = normalWoodMaterial;
		sail.material = normalSailMaterial;
     }

     // TODO Update UI
     // TODO Playsound "Clink clank kaplunk"
}
