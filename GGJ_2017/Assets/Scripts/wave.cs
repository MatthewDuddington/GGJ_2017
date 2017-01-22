using UnityEngine;
using System.Collections;

public class wave : MonoBehaviour {
  public float scaleZ;
  public float meshWidthZ;
  public float meshWidthX;
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
  private int[] boundaries;

  private void Start() {
    Generate();
  }
  public float ProbingFunction2(float x, float z, float time) {
    float distance = new Vector2(x - rippleX, z - rippleZ).magnitude;
    //return Mathf.Sin(time * speed + x * xLength + z * zLength) * scale;
    return -Mathf.Pow(Mathf.Abs(Mathf.Sin(time * speed + distance * rippleLength)), 1) * scaleZ;
  }
  public float ProbingFunction(float x, float z, float time) {

    float distance = new Vector2(x - rippleX, z - rippleZ).magnitude;
    //return Mathf.Sin(time * speed + x * xLength + z * zLength) * scale;
    return Mathf.Sin(time * speed + distance * rippleLength) * scaleZ;
  }
  public float ProbingSideWaves(float x, float z, float time) {

    return -Mathf.Pow(Mathf.Abs(Mathf.Sin(time * speed + x * xLength + z * zLength)), 1) * scaleZ;
  }

  public Vector3 CalculateNormal(float x, float z, float time) {
    float epsilon = 0.1f;
    float y1 = ProbingFunction(x - epsilon, z, time),
         y2 = ProbingFunction(x + epsilon, z, time),
         y = ProbingFunction(x, z, time);

    Vector3 v1 = new Vector3(x - epsilon, y1, 0f) - new Vector3(x + epsilon, y2, 0f);
    Vector3 v2 = new Vector3(x, y, z - epsilon) - new Vector3(x, y, z + epsilon);
    Vector3 norm = Vector3.Cross(v1, v2).normalized;
    return -norm;
  }


  private void Generate() {
    mesh = GetComponent<MeshFilter>().mesh;
    mesh.name = "Procedural Grid";

    vertices = new Vector3[(xSize + 1) * (ySize + 1)];
    boundaries = new int[(xSize + 1) * (ySize + 1)];

    for (int i = 0, y = -ySize / 2; y <= ySize / 2; y++) {
      for (int x = -xSize / 2; x <= xSize / 2; x++, i++) {
        vertices[i] = new Vector3(x * meshWidthX, 0, y * meshWidthZ);
        boundaries[i] = 0;
        if (y == -ySize / 2 || x == xSize / 2 || y == ySize / 2 || x == -xSize / 2) {
          boundaries[i] = 1;
        }
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
      Vector3 globalVertex = transform.TransformPoint(vertex);
      //vertex.y += Mathf.Sin(Time.time * speed + baseHeight[i].x + baseHeight[i].y + baseHeight[i].z + position.x + position.y + position.z) * scale;
      if (boundaries[i] == 1) {
        vertex.y = -10;
      } else {
        vertex.y = ProbingSideWaves(globalVertex.x, globalVertex.z, Time.time);
      }
      //vertex.y = ProbingFunction2(globalVertex.x, globalVertex.z, Time.time);

      //vertex.y += Mathf.PerlinNoise(baseHeight[i].x + noiseWalk, baseHeight[i].y + Mathf.Sin(Time.time * 0.1f)) * noiseStrength;
      vertices[i] = vertex;
    }
    mesh.vertices = vertices;
    mesh.RecalculateNormals();
  }
}