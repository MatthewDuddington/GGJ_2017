﻿using UnityEngine;
using System.Collections;

public class wave : MonoBehaviour {
  public float scale;
  public float speed;
  public float rippleX;
  public float rippleZ;
  public float xLength;
  public float rippleLength;
  public float zLength;
  public Transform middle;
  //public float noiseStrength;
  //public float noiseWalk;
  public int xSize;
  public int ySize;

  private Mesh mesh;
  private Vector3[] vertices;
  private Vector3[] baseHeight;

  private void Start() {
    Generate();
  }

  public float ProbingFunction(float x, float z, float time) {

    float distance = new Vector2(x-rippleX, z-rippleZ).magnitude;
    //return Mathf.Sin(time * speed + x * xLength + z * zLength) * scale;
    return Mathf.Sin(time * speed + distance * rippleLength) * scale;
  }

  private void Generate() {
    mesh = GetComponent<MeshFilter>().mesh;
    mesh.name = "Procedural Grid";

    vertices = new Vector3[(xSize + 1) * (ySize + 1)];
    for (int i = 0, y = 0; y <= ySize; y++) {
      for (int x = 0; x <= xSize; x++, i++) {
        vertices[i] = new Vector3(x,0, y);
      }
    }
    mesh.vertices = vertices;

    int[] triangles = new int[xSize * ySize * 6];
    for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++) {
      for (int x = 0; x < xSize; x++, ti += 6, vi++) {
        triangles[ti] = vi;
        triangles[ti + 3] = triangles[ti + 2] = vi + 1;
        triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
        triangles[ti + 5] = vi + xSize + 2;
      }
    }

    mesh.triangles = triangles;
    mesh.RecalculateNormals();

    print("size vertices: " + mesh.vertices.Length + " tiangle rarray size: " + mesh.triangles.Length);
  }


  void Update() {
    if (baseHeight == null)
      baseHeight = mesh.vertices;

    Vector3[] vertices = new Vector3[baseHeight.Length];

    for (int i = 0; i < vertices.Length; i++) {
      Vector3 vertex = baseHeight[i];
      
      //vertex.y += Mathf.Sin(Time.time * speed + baseHeight[i].x + baseHeight[i].y + baseHeight[i].z + position.x + position.y + position.z) * scale;
      vertex.y = ProbingFunction(baseHeight[i].x, baseHeight[i].z, Time.time);

      //vertex.y += Mathf.PerlinNoise(baseHeight[i].x + noiseWalk, baseHeight[i].y + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;
      vertices[i] = vertex;
    }
    mesh.vertices = vertices;
    mesh.RecalculateNormals();
  }
}