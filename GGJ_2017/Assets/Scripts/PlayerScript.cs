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

    private int CoinTotal = 100;
	private int numberOfCoinsToDropWhenHit = 10;
	private float CoinToWeightRatio;
	public Coin coinPrefab;

    public bool isIronclad_;
    private IronCladding ironcladding;


	// Use this for initialization
	void Start () {
          rb = GetComponent<Rigidbody>();
          moving = false;

          ironcladding = gameObject.GetComponent<IronCladding>();
	}
	
	// Update is called once per frame
	void Update () {
          if (IsControllable)
          {
               if (Input.GetKey(KeyCode.S))
               {
                    rb.AddForce(0f, 0f, -CharacterSpeed);
               }
               if (Input.GetKey(KeyCode.W))
               {
                    rb.AddForce(0f, 0f, CharacterSpeed);
               }
               if (Input.GetKey(KeyCode.D))
               {
                    rb.AddForce(CharacterSpeed, 0f, 0f);
               }
               if (Input.GetKey(KeyCode.A))
               {
                    rb.AddForce(-CharacterSpeed, 0f, 0f);
               }
               if (Input.GetKeyDown(KeyCode.Space))
                    rb.AddForce(0f, JumpStrength, 0f);
          }
    }
	
	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.GetComponent<PlayerScript>()) {
			if (coll.gameObject.GetComponent<PlayerScript>().IsIronclad()) {
				DropCoins(numberOfCoinsToDropWhenHit);
			}
			else {
				// TODO Playsound "BOING"
			}
		}
	}

	void OnTriggerEnter(Collider coll) {
		GameObject other = coll.gameObject;
		if (other.GetComponent<Coin>()) {
			print("is a coin");
			PickupCoins(other.GetComponent<Coin>().Pickup());
			Destroy(other);
		}
		else if (other.GetComponent<IronPowerup>()) {
			print("is some iron");
			other.GetComponent<IronPowerup>().Pickup();
			ironcladding.Equip();
			StartCoroutine(IroncladPowerTimer());
			Destroy(other);
		} 
		else {
			print("is not a coin or iron");
		}
	}

    public void PickupCoins(int numberOfCoins) {
    	CoinTotal += numberOfCoins;
    	// TODO Update UI
    }

    public float Weight() {
    	return CoinTotal * CoinToWeightRatio;
    }

    public bool IsIronclad() {
    	return isIronclad_;
    }

    private void DropCoins(int numberOfCoins) {
    	CoinTotal -= numberOfCoins;
    	for (int i = numberOfCoins; i > 0; i--) {
    		Coin.GetNextCoin().ThrowAway(transform);
    	}
    	// TODO Update UI
    	// TODO Playsound "Clink clank kaplunk"
    }

    private IEnumerator IroncladPowerTimer() {
    	isIronclad_ = true;
    	// TODO Playsound "Dun dun dun dun dun dun... (Jaws)"
    	yield return new WaitForSeconds(IronCladding.PowerupTime());
    	ironcladding.UnEquip();
    	isIronclad_ = false;
    }
}
