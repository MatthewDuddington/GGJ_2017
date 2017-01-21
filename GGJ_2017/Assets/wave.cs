using UnityEngine;
using System.Collections;

public class wave : MonoBehaviour
{
    public float scale = 0.1f;
    public float speed = 1.0f;
    public float length = 10.0f;
    public float noiseStrength = 1f;
    public float noiseWalk = 1f;
    Vector3 position;


    private Vector3[] baseHeight;

    private void Start()
    {
    }

    void Update()
    {

        position = transform.position;
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        if (baseHeight == null)
            baseHeight = mesh.vertices;

        Vector3[] vertices = new Vector3[baseHeight.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseHeight[i];
            
            //vertex.y += Mathf.Sin(Time.time * speed + baseHeight[i].x + baseHeight[i].y + baseHeight[i].z + position.x + position.y + position.z) * scale;
            vertex.y = Mathf.Sin(Time.time * speed + baseHeight[i].x + baseHeight[i].z + position.x  + position.z) * scale;

            //vertex.y += Mathf.PerlinNoise(baseHeight[i].x + noiseWalk, baseHeight[i].y + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}