using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeatherManagerScript : MonoBehaviour
{
     wave waveScript;
     public float waitTime;

     float timer;
     int current_stage;
     public float[] stageLengths;
     public bool changeWeatherDynamically;

     //weather 0
     private float[] Robert_Ripple = { 0.36f, 0.38f, 1.48f, 3.27f, 47.94f,
          1.27f, 2.77f, 0.27f, 0.22f,
          0.46f, 0.91f, 0.49f, 0.79f,
          0.88f, 0.3f, 0.93f };
     //weather 1
     private float[] Robert_Storm = { 0.36f, 0.38f, 1.28f, 4.0f, -5.25f,
          1.27f, 2.77f, 0.27f, 0.22f,
          0.46f, 0.91f, 0.49f, 0.76f,
          -0.28f, 0.93f, 0.51f };
     private float[] Robert_Tranq = { 0.93f, 0.53f, 0.3f, 9.46f, 27f,
          0.75f, 2.72f, 0.14f, 0.65f,
          0.34f, 0.09f, 0.49f, 0.74f,
          0.43f, 1.83f, 1.3f };
     private float[] Robert_Calm = { 0.36f, 0.38f, 1.28f, 4f, -5.25f,
          1.27f, 23.77f, 0.27f, 0.22f,
          0.46f, 0.91f, 0.49f, 0.76f,
          -0.28f, 0.93f, 0.51f };


     private float[] Nicks_Calm = { 0f, 0f, 0f, 0f, 0,
          0f, 0f, 0f, 0f,
          3.6f, 1.2f, 0.2f, 0.2f,
          0f,1f,0f };

     private float[] Nicks_Calm_Ripples =  { -1.5f, 0.24f, 3.6f, 100f, 0f,
          0, 0, 0, 0,
          3.6f, 1.2f, 0.2f, 0.2f,
          0f,0.5f,0.5f };

     private float[] Nicks_Ripples = { -1.5f, 0.24f, 3.6f, 100f, 0f,
          0, 0, 0, 0,
          3.6f, 1.2f, 0.2f, 0.2f,
          0f,0f,1f };

     private float[] Nicks_PreStorm = { -1.5f, 0.24f, 3.6f, 100f, 0f,
          2f, 7f, 0.1f, 0.1f,
          0f,0f,0f,0f,
          0.5f,0f,0.5f };

     private float[] Nicks_Storm =
          {
          10, 0.25f, 1f, 100f, 0f,
          3f, 5f, 0.14f, 0.12f,
          0f,0f,0f,0f,
          1f,0f,1f
     };

     private float[] Nicks_Tsunami =
     {
          0f, 0f, 0f, 0f, 0,
          0f, 0f, 0f, 0f,
          2f, 10f, 0.12f, 0f,
          0f,1f,0f
};

     public float[] wavePushingForces;

     public GameObject[] players;

     float[] oldWeather;
     int currentWeather;

     float[] arrayLerp(float[] arrayA, float[] arrayB, float t)
     {
          if (arrayA.Length != arrayB.Length)
          {
               print("asd!@EDA");
               return null;
          }
          else
          {
               float[] result = new float[arrayA.Length];
               for (int i = 0; i < arrayA.Length; i++)
               {
                    result[i] = Mathf.Lerp(arrayA[i], arrayB[i], t);
               }
               return result;
          }
     }

     private IEnumerator ChangeWeather(float[] newWeather)
     {
          float[] oldWeather = waveScript.BigGetter();
          for (float t = 0; t < 1; t += 0.01f)
          {
               waveScript.BigSetter(arrayLerp(oldWeather, newWeather, t));

               yield return new WaitForSeconds(waitTime);
          }
     }


     void Start()
     {
          print("length: " + Robert_Calm.Length + " " + Robert_Ripple.Length + " " + Robert_Storm.Length + " " + Robert_Tranq.Length);
          waveScript = GetComponent<wave>();
          current_stage = 0;
          currentWeather = 0;
          if (changeWeatherDynamically)
               waveScript.BigSetter(Nicks_Calm);
          timer = stageLengths[0];
     }

     void DeployRandomWeather()
     {

          int random;
          do
          {

               random = Random.Range(0, 4);
          } while (currentWeather == random);
          print("random number: " + random);


          if (random == 0)
          {
               StartCoroutine(ChangeWeather(Robert_Ripple));
               currentWeather = 0;
          }
          else if (random == 1)
          {
               StartCoroutine(ChangeWeather(Robert_Storm));
               currentWeather = 1;
          }
          else if (random == 2)
          {
               StartCoroutine(ChangeWeather(Robert_Tranq));
               currentWeather = 2;
          }
          else if (random == 3)
          {
               StartCoroutine(ChangeWeather(Robert_Calm));
               currentWeather = 3;
          }

          print("hardcode!");
     }

     void DeployNextWeather(int stageNumber)
     {
          if (stageNumber == 0)
          {
               StartCoroutine(ChangeWeather(Nicks_Calm_Ripples));
               currentWeather = 0;
          }
          else if (stageNumber == 1)
          {
               StartCoroutine(ChangeWeather(Nicks_Ripples));
               currentWeather = 1;
          }

          else if (stageNumber == 2)
          {
               StartCoroutine(ChangeWeather(Nicks_PreStorm));
               currentWeather = 2;
          }

          else if (stageNumber == 3)
          {
               StartCoroutine(ChangeWeather(Nicks_Storm));
               currentWeather = 3;
          }

          else if (stageNumber == 4)
          {
               StartCoroutine(ChangeWeather(Nicks_Calm_Ripples));
               currentWeather = 4;
          }

          else if (stageNumber == 5)
          {
               StartCoroutine(ChangeWeather(Nicks_Tsunami));
               currentWeather = 5;
          }

          //else if (stageNumber == 2)
          //{
          //     StartCoroutine(ChangeWeather(Robert_Tranq));
          //     currentWeather = 2;
          //}
          //else if (stageNumber == 3)
          //{
          //     StartCoroutine(ChangeWeather(Robert_Calm));
          //     currentWeather = 3;
          //}
          //print("hardcode!");
     }


     void Update()
     {
          if (current_stage < stageLengths.Length && changeWeatherDynamically)
          {
               timer -= Time.deltaTime;
               if (timer < 0)
               {
                    timer = stageLengths[current_stage];
                    //DeployRandomWeather();
                    DeployNextWeather(current_stage);
                    players[0].GetComponent<BoatInteraction>().WavePushForce = wavePushingForces[current_stage];

                    current_stage++;
                    if (current_stage > 5)
                         current_stage = 0;
               }
          }
     }
}
