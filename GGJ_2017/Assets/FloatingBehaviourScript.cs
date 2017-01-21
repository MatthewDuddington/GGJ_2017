using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBehaviourScript : MonoBehaviour {

     private Rigidbody rb;
     private wave water;
     public Transform[] floatingPoints;
     public float floatingMultiplier;
     public float objectHalfHeight;
     float zeroWaterLevel;

     // Use this for initialization
     void Start () {
          rb = GetComponent<Rigidbody>();
          water = GameObject.FindWithTag("Water").GetComponent<wave>();
          zeroWaterLevel = water.transform.position.y;
     }

     // Update is called once per frame
     void Update()
     {
          simulateFloating();
     }


     void simulateFloating()
     {
          if (water)
          {
               foreach (Transform point in floatingPoints)
               {
                    float waterLevel = water.ProbingFunction(point.position.x, point.position.z, Time.time) - zeroWaterLevel;
                    float currentYLocation = point.position.y;
                    if (currentYLocation < waterLevel)
                    {
                         Vector3 forceAmount = new Vector3(0f, (waterLevel - currentYLocation) * floatingMultiplier + objectHalfHeight, 0f);
                         rb.AddForceAtPosition(forceAmount / floatingPoints.Length, point.transform.position);
                    }
               }
          }
     }

}
