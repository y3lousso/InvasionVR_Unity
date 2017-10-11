using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    [Header("Mesh settings")]
    public int mapWidth;
    public int mapHeight;
    public float stepBetweenVertices;
    public float randomizeVecticesPos;
    public MeshRenderer mr;
    public MeshFilter mf;
    public Mesh mesh;

    [Header("Noise settings")]
    public float noiseScale;
    public int octaves;
    public float persistance;
    public float lacunarity;

    public Vector3[] vertices;

    public void GenerateMap()
    {
        vertices = new Vector3[mapWidth * mapHeight];
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, noiseScale, octaves, persistance,lacunarity);

        for (int y = 0; y < mapHeight; y++)     
        {
            for (int x = 0; x < mapWidth; x++)
            {
                vertices[x+y*mapWidth] = new Vector3(stepBetweenVertices*x, noiseMap[x, y], stepBetweenVertices*y);
            }
        }

        DrawMesh();
    }

    public void DrawMesh()
    {
        int[] triangleIndices = new int[mapWidth*mapHeight*6];
        int ti = 0;
        for (int y = 0; y < mapHeight-1; y++)
        {
            for (int x = 0; x < mapWidth-1; x++)
            {
                int a = x + mapWidth + y * mapWidth;
                int b = x + y * mapWidth;
                int c = x + 1 + y * mapWidth ;
                int d = x + mapWidth + 1 + y * mapWidth;
                triangleIndices[ti] = a; ti++;
                triangleIndices[ti] = d; ti++;
                triangleIndices[ti] = c; ti++;
                triangleIndices[ti] = c; ti++;
                triangleIndices[ti] = b; ti++;
                triangleIndices[ti] = a; ti++;
            }
        }

        if (mesh == null)
        {
            mesh = new Mesh();
        }else
        {
            mesh.Clear();
        }

        mesh.vertices = vertices;
        mesh.triangles = triangleIndices;
        mesh.uv = new Vector2[mapWidth * mapHeight];
        mf.sharedMesh = mesh;
        mesh.RecalculateNormals();
    }


}
