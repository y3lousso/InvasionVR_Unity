using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtrudeShape : MonoBehaviour {

    public Vector2[] verts;
    public Vector2[] normals;
    public float[] us;
    public int[] lines;

    public void Reset()
    {
        verts = new Vector2[] {
            new Vector2(0f, 0f),
            new Vector2(.1f, 0f),
        };
        normals = new Vector2[] {
            new Vector2(0f, 1f),
            new Vector2(0f, 1f),
        };
        us = new float[] {
            0f,
            1f
        };
        lines = new int[]
        {
            0, 1
        };
}

    public void AddPoint()
    {
        
    }

    public Vector3 GetControlPoint(int index)
    {
        return new Vector3(verts[index].x, verts[index].y, 0f);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
