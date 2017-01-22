using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager gameManager_;

	public GameObject coinPrefab;
	public GameObject ironPowerupPrefab;

	public GameObject coins;
	public GameObject ironPowerups;

	void Start() {
		//Coin.LoadCoins();
		//IronPowerup.LoadIronPowerups();
	}

	public static GameManager gameManager() {
		if (gameManager_ == null) {
			gameManager_ = GameObject.FindObjectOfType<GameManager>();
		}
		return gameManager_;
	}

}
