using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronCladding : MonoBehaviour {

	static private float powerupTime_ = 100;

	void Start() {
		enabled = false;
	}

	static public float PowerupTime() {
		return powerupTime_;
	}

	public void Equip() {
		enabled = true;
		// TODO Playsound "KLANG Hammer Hammer"
	}

	public void UnEquip() {
		enabled = false;
	}

	void OnCollision(Collision coll) {
		if (coll.gameObject.GetComponent<PlayerScript>() || coll.gameObject.GetComponent<IronCladding>()) {
			enabled = false;
		}
	}
}
