using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SplineDecorator))]
public class SplineDecoratorEditor : Editor
{
    private SplineDecorator splineDecorator;

    public override void OnInspectorGUI()
    {
        splineDecorator = target as SplineDecorator;
        DrawDefaultInspector();
        if (GUILayout.Button("Update"))
        {
            Undo.RecordObject(splineDecorator, "Update");
            splineDecorator.UpdateDecoration();
            EditorUtility.SetDirty(splineDecorator);
        }
        if (GUILayout.Button("Clear"))
        {
            Undo.RecordObject(splineDecorator, "Clear");
            splineDecorator.ForceClearDecoration();
            EditorUtility.SetDirty(splineDecorator);
        }

    }


}


