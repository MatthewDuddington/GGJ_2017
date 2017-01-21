using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

     private static Coin[] coins = new Coin[100];
     private static int currentCoinsIndex = -1;

     public int value = 10;

     private bool isColectable_;
     private Rigidbody rigidbody;

     void Start()
     {
          enabled = false;
          isColectable_ = true;
          rigidbody = gameObject.GetComponent<Rigidbody>();
     }

     public void ThrowAway(Transform shipLocation)
     {
          // Propel coin in random direction
          rigidbody.isKinematic = false;
          transform.position = shipLocation.position + new Vector3(0, 15, 0);
          transform.Rotate(Vector3.up, Random.Range(0f, 359.999f));
          rigidbody.AddRelativeForce(new Vector3(1000 + Random.Range(0, 1500), 1500 + Random.Range(0, 3000), 0));  //TODO define propper force
          transform.Rotate(Vector3.up * Random.Range(0f, 359.999f));
     }

     public static Coin GetNextCoin()
     {
          currentCoinsIndex++;
          if (currentCoinsIndex == coins.Length)
          {
               currentCoinsIndex = 0;
          }
          return coins[currentCoinsIndex];
     }

     public int GetValue()
     {
          return value;
     }

     public static void LoadCoins()
     {
          for (int i = 0; i < coins.Length; i++)
          {
               GameObject newCoin = Instantiate(GameManager.gameManager().coinPrefab, GameManager.gameManager().coins.transform);
               coins[i] = newCoin.GetComponent<Coin>();
          }

          //TODO Randomly distribute around level
     }

}
