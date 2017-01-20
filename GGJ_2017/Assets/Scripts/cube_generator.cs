using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cube_generator : MonoBehaviour
{
    public GameObject prefabCube;
    public GameObject[,] cubeGrid;

    public int x_grid_size = 10;
    public int z_grid_size = 10;

    public float length = 0.1f;
    public float speed = 0.1f;
    public float amplitude = 10.0f;

    // Use this for initialization
    void Start()
    {
        // Create Gameobjects

        cubeGrid = new GameObject[x_grid_size, z_grid_size];

        for (int x = 0; x != x_grid_size; x++)
        {
            for (int z = 0; z != z_grid_size; z++)
            {
                cubeGrid[x, z] = Instantiate(prefabCube, new Vector3(x - x_grid_size/2, 0f, z - x_grid_size/2), Quaternion.identity);

                //cubeGrid[x][z] = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //cubeGrid[x][z].transform.position = new Vector3(x*1.1f,0, z*1.1f);
            }

        }

        //GameObject test = Instantiate(prefabCube, new Vector3(1, 0.5f, 1), Quaternion.identity);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        for (int x = 0; x != x_grid_size; x++)
        {
            for (int z = 0; z != z_grid_size; z++)
            {
                Vector3 current_position = cubeGrid[x, z].transform.position;
           
                Vector3 new_position = new Vector3(current_position.x, amplitude * Mathf.Sin(Time.time * speed + current_position.x * length) * Mathf.Sin(Time.time * speed + current_position.z * length), current_position.z);
                //print("amp " + amplitude);
                //print(amplitude * Mathf.Sin(Time.time * speed + current_position.x * length));
                cubeGrid[x, z].transform.position = new_position;
                //cubeGrid[x, z].transform.Translate(new Vector3(0, Mathf.Sin(Time.time),0));
            }
        }
    }
}