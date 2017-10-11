using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SplineDecorator2D))]
public class SplineDecorator2DEditor : Editor
{
    private SplineDecorator2D splineDecorator2D;

    public override void OnInspectorGUI()
    {
        splineDecorator2D = target as SplineDecorator2D;
        DrawDefaultInspector();
        if (GUILayout.Button("GenerateMesh"))
        {
            Undo.RecordObject(splineDecorator2D, "GenerateMesh");
            splineDecorator2D.GenerateMesh();
            EditorUtility.SetDirty(splineDecorator2D);
        }

    }


}


