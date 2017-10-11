using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(BezierSpline))]
public class SplineDecorator2D : MonoBehaviour {

    public MeshRenderer mr;
    public MeshFilter mf;
    public Mesh mesh;
    public ExtrudeShape shape;
    private OrientedPoint[] path;
    public BezierSpline bezierSpline;
    public int step = 20;

    public void ExtrudeMesh()
    {
        int vertsInShape = shape.verts.Length;
        int segments = path.Length-1; //step -1
        int edgeLoops = path.Length;
        int vertCount = vertsInShape * edgeLoops;
        int triCount = shape.lines.Length* segments;
        int triIndexCount = triCount * 3;

        int[] triangleIndices = new int[triIndexCount];
        Vector3[] vertices = new Vector3[vertCount];
        Vector3[] normals = new Vector3[vertCount];
        Vector2[] uvs = new Vector2[vertCount];

        for (int i = 0; i < path.Length; i++)
        {
            path[i].CorrectOffset(transform.position);
            int offset = i * vertsInShape;
            for (int j = 0; j < vertsInShape; j++)
            {
                int id = offset + j;       
                vertices[id] = path[i].LocalToWorld(shape.verts[j]);
                normals[id] = path[i].LocalToWorldDirection(shape.normals[j]);
                uvs[id] = new Vector2(shape.us[j], i / (float)edgeLoops);
            }
        }
        int ti = 0;
        for (int i = 0; i < segments; i++)
        {
            int offset = i * vertsInShape;
            for (int l = 0; l < shape.lines.Length; l +=2)
            {
                int a = offset + shape.lines[l] + vertsInShape;
                int b = offset + shape.lines[l];
                int c = offset + shape.lines[l+1];
                int d = offset + shape.lines[l+1] + vertsInShape;
                triangleIndices[ti] = a; ti++;
                triangleIndices[ti] = d; ti++;
                triangleIndices[ti] = c; ti++;
                triangleIndices[ti] = c; ti++;
                triangleIndices[ti] = b; ti++;
                triangleIndices[ti] = a; ti++;
            }
        }

        mesh.Clear();
        foreach(var i in vertices)
        {
            Debug.Log(i);
        }
        mesh.vertices = vertices;
        mesh.triangles = triangleIndices;
        mesh.normals = normals;
        mesh.uv = uvs;
        mesh.name = "Mesh";
        mf.sharedMesh = mesh;
        mesh.RecalculateNormals();
    }

    public void GenerateMesh()
    {
        // Set path
        mesh = new Mesh();
        Array.Resize(ref path, step+1);
        for (int i = 0; i <= step; i++)
        {
            path[i] = new OrientedPoint(bezierSpline.GetPoint((float)i / step), bezierSpline.GetOrientation3D((float)i/step));
        }
        ExtrudeMesh();
    }





    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
