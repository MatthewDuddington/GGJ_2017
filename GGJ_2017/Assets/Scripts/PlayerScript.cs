using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

     public float CharacterSpeed;
     public float JumpStrength;

     private Rigidbody rb;

     private bool moving;
     private Vector3 destination;


	// Use this for initialization
	void Start () {
          rb = GetComponent<Rigidbody>();
          moving = false;
	}
	
	// Update is called once per frame
	void Update () {

          if (moving)
          {
               
          }
		if (Input.GetKey(KeyCode.W))
          {
               rb.AddForce(0f, 0f, -CharacterSpeed);
          }
          else if (Input.GetKey(KeyCode.S))
          {
               rb.AddForce(0f, 0f, CharacterSpeed);
          }
          else if (Input.GetKey(KeyCode.A))
          {
               rb.AddForce(CharacterSpeed, 0f, 0f);
          }
          else if (Input.GetKey(KeyCode.D))
          {
               rb.AddForce(-CharacterSpeed, 0f, 0f);
          }
          else if (Input.GetKeyDown(KeyCode.Space))
               rb.AddForce(0f, JumpStrength, 0f);
     }

}
