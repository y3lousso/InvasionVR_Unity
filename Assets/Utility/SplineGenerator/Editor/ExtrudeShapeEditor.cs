using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ExtrudeShape))]
public class ExtrudeShapeEditor : Editor
{
    private ExtrudeShape extrudeShape;
    private Transform handleTransform;
    private Quaternion handleRotation;

    private const float handleSize = .04f;
    private const float pickSize = .06f;
    private int selectedIndex = -1;

    public override void OnInspectorGUI()
    {
        extrudeShape = target as ExtrudeShape;
        DrawDefaultInspector();
        
        

    }

    private void OnSceneGUI()
    {
        extrudeShape = target as ExtrudeShape;
        handleTransform = extrudeShape.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;

        Vector3 p0 = ShowPoint(0);
        for (int i = 0; i < extrudeShape.verts.Length-1; i ++)
        {
            Vector3 p1 = ShowPoint(i+1);
            Handles.DrawLine(p0, p1);
            Repaint();
            p0 = p1;
        }

    }

    Vector3 ShowPoint(int index)
    {
        Vector3 point = handleTransform.TransformPoint(extrudeShape.GetControlPoint(index));
        Handles.color = Color.grey;
        float size = HandleUtility.GetHandleSize(point);
        if (index == 0)
        {
            size *= 2f;
        }
        if (Handles.Button(point, handleRotation, size * handleSize, size * pickSize, Handles.DotHandleCap))
        {
            selectedIndex = index;
            Repaint();
        }
      /*  if (selectedIndex == index)
        {
            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(extrudeShape, "MovePoint");
                EditorUtility.SetDirty(extrudeShape);
                extrudeShape.SetControlPoint(selectedIndex, handleTransform.InverseTransformPoint(point));
            }
        }*/
        return point;
    }

    void ShowLine(int index)
    {

    }



}


