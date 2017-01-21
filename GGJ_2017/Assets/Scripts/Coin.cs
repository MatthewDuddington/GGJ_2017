using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	private static Coin[] coins = new Coin[100];
	private static int currentCoinsIndex = -1;

	public int value = 10;

     private bool isColectable_;
	private Rigidbody rb;

	void Start() {
		enabled = false;
          isColectable_ = true;
		rb = gameObject.GetComponent<Rigidbody>();
	}

	public int Pickup() {
		if (isColectable_) {
			// TODO Playsound "Ding"
			this.enabled = false;
			return value;
		}
		else {
			print("invalid coin pickup call");
			return 0;
		}
	}

	public void ThrowAway(Transform shipLocation) {
		// Propel coin in random direction
		rb.isKinematic = false;
		transform.position = shipLocation.position + new Vector3(0, 15, 0);
		transform.Rotate(Vector3.up, Random.Range(0f, 359.999f));
		rb.AddRelativeForce(new Vector3(1000 + Random.Range(0, 1500),1500 + Random.Range(0, 3000),0));  //TODO define propper force
		transform.Rotate(Vector3.up * Random.Range(0f, 359.999f));
	}

	public static Coin GetNextCoin() {
		currentCoinsIndex++;
		if (currentCoinsIndex == coins.Length) {
			currentCoinsIndex = 0;
		}
		return coins[currentCoinsIndex];
	}

	public static void LoadCoins() {
		for (int i = 0; i < coins.Length; i++) {
			GameObject newCoin = Instantiate(GameManager.gameManager().coinPrefab, GameManager.gameManager().coins.transform);
			coins[i] = newCoin.GetComponent<Coin>();
		}

		//TODO Randomly distribute around level
	}

}
