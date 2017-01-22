using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldSpawner : MonoBehaviour {
  public int timeScattering;
  public int constantPeriod;
  public GameObject goldCoin;
  public GameObject goldParent;

  float timer;
  private wave waveScript;

  public float Period() {
    return Random.Range(0, timeScattering) + constantPeriod;
  }

  // Use this for initialization

  void Awake() {
    waveScript = GetComponent<wave>();
    if (!waveScript) {
      print("no wave script!");
    }
  }
  
  void Start () {
    timer = Period();
	}
	
  void Spawn() {
 
    float xHalfWidth = 2* waveScript.xSize * waveScript.meshWidthX;
    float zHalfWidth = 2* waveScript.ySize * waveScript.meshWidthZ;

    float x = transform.position.x + Random.Range(-xHalfWidth, xHalfWidth);
    float z = transform.position.z + Random.Range(-zHalfWidth, zHalfWidth);
    Instantiate(goldCoin, new Vector3(x, 100, z), Quaternion.identity, goldParent.transform);
  }
	// Update is called once per frame
	void Update () {
    timer -= Time.deltaTime;
    if ( timer < 0) {
      timer = Period();
      Spawn();
      print("SPAWN!");
    }
	}
}
