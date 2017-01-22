using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeatherManagerScript : MonoBehaviour {
  wave waveScript;
  public float waitTime;

  float timer;
  int current_stage;
  public float[] stageLengths;

  //weather 0

  private float[] Robert_Ripple = { 0.36f, 0.38f, 1.48f, 3.27f, 47.94f, 1.27f, 2.77f, 0.27f, 0.22f, 0.46f, 0.91f, 0.49f, 0.79f, 0.88f, 0.3f, 0.93f };
  //weather 1

  private float[] Robert_Storm = { 0.36f, 0.38f, 1.28f, 4.0f, -5.25f, 1.27f, 2.77f, 0.27f, 0.22f, 0.46f, 0.91f, 0.49f, 0.76f, -0.28f, 0.93f, 0.51f };
  private float[] Robert_Tranq = { 0.93f, 0.53f, 0.3f, 9.46f, 27f, 0.75f, 2.72f, 0.14f, 0.65f, 0.34f, 0.09f, 0.49f, 0.74f, 0.43f, 1.83f, 1.3f };
  private float[] Robert_Calm = { 0.36f, 0.38f, 1.28f, 4f, -5.25f, 1.27f, 23.77f, 0.27f, 0.22f, 0.46f, 0.91f, 0.49f, 0.76f, -0.28f, 0.93f, 0.51f };
  float[] oldWeather;
  int currentWeather;

  float[] arrayLerp(float[] arrayA, float[] arrayB, float t) {
    if (arrayA.Length != arrayB.Length) {
      print("asd!@EDA");
      return null;
    } else {
      float[] result = new float[arrayA.Length];
      for (int i = 0; i < arrayA.Length; i++) {
        result[i] = Mathf.Lerp(arrayA[i], arrayB[i], t);
      }
      return result;
    }
  }

  private IEnumerator ChangeWeather(float[] newWeather) {
    float[] oldWeather = waveScript.BigGetter();
    for (float t = 0; t < 1; t += 0.01f) {
      waveScript.BigSetter(arrayLerp(oldWeather, newWeather, t));

      yield return new WaitForSeconds(waitTime);
    }
  }


  void Start() {
    print("length: " +  Robert_Calm.Length + " " + Robert_Ripple.Length + " " + Robert_Storm.Length + " " + Robert_Tranq.Length);
    waveScript = GetComponent<wave>();
    current_stage = 0;
    currentWeather = 0;
    waveScript.BigSetter(Robert_Calm);
    timer = stageLengths[0];
  }

  void DeployRandomWeather() {

    int random;
    do {

      random = Random.Range(0, 4);
    } while (currentWeather == random);
    print("random number: " + random);


    if (random == 0) {
      StartCoroutine(ChangeWeather(Robert_Ripple));
      currentWeather = 0;
    } else if (random == 1) {
      StartCoroutine(ChangeWeather(Robert_Storm));
      currentWeather = 1;
    } else if (random == 2) {
      StartCoroutine(ChangeWeather(Robert_Tranq));
      currentWeather = 2;
    } else if (random == 3) {
      StartCoroutine(ChangeWeather(Robert_Calm));
      currentWeather = 3;
    }

    print("hardcode!");
  }

  void Update() {
    if (current_stage < stageLengths.Length) {
      timer -= Time.deltaTime;
      if (timer < 0) {
        timer = stageLengths[current_stage];
        DeployRandomWeather();
        current_stage++;
      }
    }
  }
}
