using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	private static Coin[] coins = new Coin[100];
	private static int currentCoinsIndex = -1;

	public int value = 10;

	private bool isColectable_ = false;
	private Rigidbody rigidbody;

	void Start() {
		enabled = false;
		rigidbody = gameObject.GetComponent<Rigidbody>();
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
		transform.position = shipLocation.position + new Vector3(0, 2, 0);
		transform.Rotate(Vector3.up, Random.Range(0f, 359.999f));
		rigidbody.AddRelativeForce(new Vector3(3000,5000,0));  //TODO define propper force
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Water") {
			isColectable_ = true;
		}
	}

	public static Coin GetNextCoin() {
		currentCoinsIndex++;
		if (currentCoinsIndex == coins.Length) {
			currentCoinsIndex = 0;
		}
		return coins[currentCoinsIndex];
	}

	public static void LoadCoins() {
		for(int i = 0; i < coins.Length; i++) {
			coins[i] = new Coin();
		}

		//TODO Randomly distribute around level
	}

}
