using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
     private static int currentCoinsIndex = -1;

     public int value = 10;

     private bool isColectable_;
     private Rigidbody rb;

     void Start()
     {
          enabled = false;
          isColectable_ = true;
          rb = gameObject.GetComponent<Rigidbody>();
     }

     public void ThrowAway(Transform shipLocation)
     {
          // Propel coin in random direction
          transform.position = shipLocation.position + new Vector3(0, 15, 0);
          transform.Rotate(Vector3.up, Random.Range(0f, 359.999f));
          rb.AddRelativeForce(new Vector3(1000 + Random.Range(0, 1500),
               1500 + Random.Range(0, 3000), 0));  //TODO define propper force
          transform.Rotate(Vector3.up * Random.Range(0f, 359.999f));
     }

     public int GetValue()
     {
          return value;
     }

}
