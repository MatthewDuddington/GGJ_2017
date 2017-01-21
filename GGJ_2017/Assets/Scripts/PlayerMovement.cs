using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public int playerID;

    public float movement_speed = 12;
    public float rotation_speed = 0.05f; 


    private Rigidbody rb;

    private float hori_axis;
    private float vert_axis; 

    private Vector3 player_rotation;
    private Vector3 joystick_position; 
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
       
	}
	
	// Update is called once per frame
	void Update () {
        if(playerID == 1)
        {

            hori_axis = Input.GetAxis("p1_Horizontal");
            vert_axis = Input.GetAxis("p1_Vertical");
        }
        else if(playerID == 2)
        {

            hori_axis = Input.GetAxis("p2_Horizontal");
            vert_axis = Input.GetAxis("p2_Vertical");
        }


        //forward_speed = Input.GetAxis("Horizontal");
        //rb.AddForce(0f, 0f, forward_speed * movement_speed);

        if (Mathf.Abs(hori_axis) >= 0.3f || Mathf.Abs(vert_axis) >= 0.3)
        {
            rb.AddRelativeForce(movement_speed, 0.0f, 0.0f);

        }


        joystick_position = new Vector3(hori_axis, 0.0f, vert_axis);
        player_rotation = transform.forward;

        //player_rotation = Mathf.Atan(hori_axis / vert_axis);

        //rb.AddTorque(Vector3.Lerp(player_rotation, joystick_position, 0.1f));
        //transform.Rotate();
        transform.forward = Vector3.Lerp(player_rotation, joystick_position, rotation_speed);

    }

}
