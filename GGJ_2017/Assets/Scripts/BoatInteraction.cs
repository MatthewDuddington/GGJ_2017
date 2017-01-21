using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

     // Use this for initialization
     void Start()
     {
          rb = GetComponent<Rigidbody>();
          collidingObject = null;
          water = GameObject.FindWithTag("Water").GetComponent<wave>();
          xMax = (water.gameObject.transform.localScale * water.xSize / 20).x;
          xMin = -xMax;

          zMax = (water.gameObject.transform.localScale * water.ySize / 20).z;
          zMin = -zMax;
          print(xMax);

          ironcladding = gameObject.GetComponent<IronCladding>();
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
          print("collision");
          if (tag == "Player")
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
                    BoatInteraction otherBoatScript = collidingObject.GetComponent<BoatInteraction>();

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

                    //drop coins
                    DropCoins(numberOfCoinsToDropWhenHit);
               }
          }
          else if (tag == "Coin")
          {
               print("is a coin");
               PickupCoins(collision.gameObject.GetComponent<Coin>().Pickup());
               collision.gameObject.SetActive(false);

          }
          else if (tag == "Iron")
          {
               print("is some iron");
               collision.gameObject.GetComponent<IronPowerup>().Pickup();
               ironcladding.Equip();
               StartCoroutine(IroncladPowerTimer());
               Destroy(collision.gameObject);
          }
     }

     private void DropCoins(int numberOfCoins)
     {
          CoinTotal -= numberOfCoins;
          for (int i = numberOfCoins; i > 0; i--)
          {
               Coin.GetNextCoin().ThrowAway(transform);
          }
     }

     private int CoinTotal = 100;
     private int numberOfCoinsToDropWhenHit = 10;
     private float CoinToWeightRatio;

     public bool isIronclad_;
     private IronCladding ironcladding;

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


     // TODO Update UI
     // TODO Playsound "Clink clank kaplunk"


     private IEnumerator IroncladPowerTimer()
     {
          isIronclad_ = true;
          // TODO Playsound "Dun dun dun dun dun dun... (Jaws)"
          yield return new WaitForSeconds(IronCladding.PowerupTime());
          ironcladding.UnEquip();
          isIronclad_ = false;
     }
}
