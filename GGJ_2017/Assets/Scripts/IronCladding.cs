using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronCladding : MonoBehaviour {

	static private float powerupTime_ = 6;

	static public float PowerupTime() {
		return powerupTime_;
	}

	public void Pickup() {
		// TODO Playsound "KLANG Hammer Hammer"
	}
}
