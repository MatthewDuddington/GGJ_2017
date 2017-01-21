using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronPowerup : MonoBehaviour {

	private static IronPowerup[] ironPowerups = new IronPowerup[10];
	private static int currentIronPowerupIndex = -1;

	void Start () {
		enabled = false;
	}
	
	public void Pickup() {
		enabled = false;
	}

	public static IronPowerup GetNextIronPowerup() {
		currentIronPowerupIndex++;
		if (currentIronPowerupIndex == ironPowerups.Length) {
			currentIronPowerupIndex = 0;
		}
		return ironPowerups[currentIronPowerupIndex];
	}
}
