using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float CharacterSpeed;
    public float JumpStrength;

    private Rigidbody rb;

    private bool moving;
    private Vector3 destination;

    public bool IsControllable;

    private int CoinTotal = 0;
	private int numberOfCoinsToDropWhenHit = 10;

    private bool isIronclad_;


	// Use this for initialization
	void Start () {
          rb = GetComponent<Rigidbody>();
          moving = false;
	}
	
	// Update is called once per frame
	void Update () {
          if (IsControllable)
          {
               if (Input.GetKey(KeyCode.W))
               {
                    rb.AddForce(0f, 0f, -CharacterSpeed);
               }
               if (Input.GetKey(KeyCode.S))
               {
                    rb.AddForce(0f, 0f, CharacterSpeed);
               }
               if (Input.GetKey(KeyCode.A))
               {
                    rb.AddForce(CharacterSpeed, 0f, 0f);
               }
               if (Input.GetKey(KeyCode.D))
               {
                    rb.AddForce(-CharacterSpeed, 0f, 0f);
               }
               if (Input.GetKeyDown(KeyCode.Space))
                    rb.AddForce(0f, JumpStrength, 0f);
          }
    }
	
	void OnCollisionEnter(Collision coll) {
		GameObject other = coll.gameObject;
		if (other is Coin) {
			PickupCoins(other.GetComponent<Coin>().Pickup());
		}
		else if (other is IronCladding) {
			other.GetComponent<IronCladding>().Pickup());
			StartCoroutine(IroncladPowerTimer());
		}
		else if (other.gameObject is PlayerScript) {
			if (other.gameObject.GetComponent<PlayerScript>().IsIronclad()) {
				DropCoins(numberOfCoinsToDropWhenHit);
			}
			else {
				// TODO Playsound "BOING"
			}
		}
	}

    public void PickupCoins(int numberOfCoins) {
    	CoinTotal += numberOfCoins;
    	// TODO Update UI
    }

    public bool IsIronclad() {
    	return isIronclad_;
    }

    private void DropCoins(int numberOfCoins) {
    	CoinTotal -= numberOfCoins;
    	// TODO Spawn a set of coins around the boat (use Coin.ThrowAway() after instantiating)
    	// TODO Update UI
    	// TODO Playsound "Clink clank kaplunk"
    }

    IEnumerator IroncladPowerTimer() {
    	isIronclad_ = true;
    	// TODO Playsound "Dun dun dun dun dun dun... (Jaws)"
    	yield return new WaitForSeconds(IronCladding.PowerupTime());
    	isIronclad_ = false;
    }
}
