using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public int playerID;



    private Rigidbody rb;

    private float hori_joystick_axis;
    private float vert_joystick_axis;
    public float joy_movement_speed = 12;
    public float joy_rotation_speed = 0.05f;


    private float key_forwards;
    private float key_rotation;
    public float key_movement_speed = 50;
    public float key_rotation_speed = 100;

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

            hori_joystick_axis = Input.GetAxis("p1_Horizontal");
            vert_joystick_axis = Input.GetAxis("p1_Vertical");
            key_forwards = Input.GetAxis("p1_key_forward");
            key_rotation = Input.GetAxis("p1_key_rotate");
        }
        else if(playerID == 2)
        {

            hori_joystick_axis = Input.GetAxis("p2_Horizontal");
            vert_joystick_axis = Input.GetAxis("p2_Vertical");
            key_forwards = Input.GetAxis("p2_key_forward");
            key_rotation = Input.GetAxis("p2_key_rotate");
        }

        joystick_movement(hori_joystick_axis, vert_joystick_axis);
        keyboard_movement();


    }

    void joystick_movement(float hori_axis, float vert_axis)
    {

        //forward_speed = Input.GetAxis("Horizontal");
        //rb.AddForce(0f, 0f, forward_speed * movement_speed);

        if (Mathf.Abs(hori_axis) >= 0.3f || Mathf.Abs(vert_axis) >= 0.3)
        {
            rb.AddRelativeForce(joy_movement_speed, 0.0f, 0.0f);
        }


        joystick_position = new Vector3(hori_axis, 0.0f, vert_axis);
        player_rotation = transform.forward;

        //player_rotation = Mathf.Atan(hori_axis / vert_axis);

        //rb.AddTorque(Vector3.Lerp(player_rotation, joystick_position, 0.1f));
        //transform.Rotate();
        transform.forward = Vector3.Lerp(player_rotation, joystick_position, joy_rotation_speed);
    }

    void keyboard_movement()
    {
        rb.AddRelativeForce(key_movement_speed * key_forwards, 0, 0);
        transform.Rotate(0,Time.deltaTime * key_rotation * key_rotation_speed, 0);

    }
}
