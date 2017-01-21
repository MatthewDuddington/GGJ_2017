using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	public int value = 10;

	public int Pickup() {
		// TODO Playsound "Ding"
		this.enabled = false;
		return value;
	}

	public void ThrowAway() {
		// TODO Propel coin in random direction
	}
}
