using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float movement_speed;
    private Rigidbody rb;

    private float hori_axis;
    private float vert_axis; 


    public float forward_speed;

    private Vector3 player_rotation;
    private Vector3 joystick_position; 
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
       
	}
	
	// Update is called once per frame
	void Update () {

        hori_axis = Input.GetAxis("Horizontal");
        vert_axis = Input.GetAxis("Vertical");

        //forward_speed = Input.GetAxis("Horizontal");
        //rb.AddForce(0f, 0f, forward_speed * movement_speed);

        if (hori_axis >= 0.3f || vert_axis >= 0.3)
        {
        rb.AddRelativeForce(movement_speed, 0.0f, 0.0f);

        }


        joystick_position = new Vector3(hori_axis, 0.0f, vert_axis);
        player_rotation = transform.forward;

        //player_rotation = Mathf.Atan(hori_axis / vert_axis);

        //rb.AddTorque(Vector3.Lerp(player_rotation, joystick_position, 0.1f));
        //transform.Rotate();
        transform.forward = Vector3.Lerp(player_rotation, joystick_position, 0.01f);
        
        print(Vector3.Lerp(player_rotation, joystick_position, 0.5f));
    }
}
