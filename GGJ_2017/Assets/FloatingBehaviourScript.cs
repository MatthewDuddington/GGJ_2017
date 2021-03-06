﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBehaviourScript : MonoBehaviour {

     private Rigidbody rb;
     private wave water;
     public Transform[] floatingPoints;
     public float floatingMultiplier;
     public float objectHalfHeight;

     public float verticalAdjustment;
     float zeroWaterLevel;


    private float xMax;
    private float xMin;
    private float zMax;
    private float zMin;

    // Use this for initialization
    void Start () {
          rb = GetComponent<Rigidbody>();
          water = GameObject.FindWithTag("Water").GetComponent<wave>();
        zeroWaterLevel = water.transform.position.y;
        xMax = (water.gameObject.transform.localScale * water.xSize / 20).x;
        xMin = -xMax;

        zMax = (water.gameObject.transform.localScale * water.ySize / 20).z;
        zMin = -zMax;
    }

     // Update is called once per frame
     void Update()
     {
          float x = transform.position.x,
              z = transform.position.z;

          if (x < xMax && x > xMin && z < zMax && z > zMin)
          {
               simulateFloating();
          }
          //else
          //    print("off the map");
     }


     void simulateFloating()
     {          
          if (water)
          {
               foreach (Transform point in floatingPoints)
               {
                    float waterLevel = water.LinearWeatherCombination(point.position.x, point.position.z, Time.time);
                    float currentYLocation = point.position.y - zeroWaterLevel - verticalAdjustment;
                    if (currentYLocation < waterLevel)
                    {
                         Vector3 forceAmount = new Vector3(0f, (waterLevel - currentYLocation) * floatingMultiplier + objectHalfHeight, 0f);
                         rb.AddForceAtPosition(forceAmount / floatingPoints.Length, point.transform.position);
                    }
               }
          }
     }

}
