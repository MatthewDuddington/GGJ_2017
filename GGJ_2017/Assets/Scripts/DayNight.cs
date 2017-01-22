using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNight : MonoBehaviour {

  public float timeCycleSpeed;

  public float dayLength;

  private float timer;
	void Start () {
    timer = dayLength;
    
  }
	
  void Update() {
    timer -= Time.deltaTime;
    if (timer < 0) {
      timer = dayLength;
      
      StartCoroutine(TimeCycle());
    }
  }


	IEnumerator TimeCycle() {
    print("boom");
    float rotationAmount = 0;
		while (rotationAmount < 180) {
      float rotationDelta = (timeCycleSpeed * Time.deltaTime);
      rotationAmount += rotationDelta;
			transform.Rotate( Vector3.forward * rotationDelta );
			yield return new WaitForFixedUpdate();
		}
	}
}
