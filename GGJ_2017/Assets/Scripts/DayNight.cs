using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour {

	private bool isTimePassing = true;

	private float timeCycleSpeed = 60;

	void Start () {
		StartCoroutine(TimeCycle());
	}
	
	IEnumerator TimeCycle() {
		while (isTimePassing) {
			transform.Rotate(Vector3.right * (360 / timeCycleSpeed * Time.deltaTime));
			yield return new WaitForFixedUpdate();
		}
	}
}
