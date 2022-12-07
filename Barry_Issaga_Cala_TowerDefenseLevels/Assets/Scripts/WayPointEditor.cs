using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(WayPoint))]
public class WayPointEditor : Editor
{
    WayPoint wayPoint => target as WayPoint;

    private void OnSceneGUI()
    {

        Handles.color = Color.yellow;
        for (int i = 0; i < wayPoint.waypointList.Count; i++)
        {
            EditorGUI.BeginChangeCheck();

            Vector3 handleWayPoint = Handles.FreeMoveHandle(wayPoint.waypointList[i].position,0.7f,Vector3.zero,Handles.SphereHandleCap);
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontSize = 14;
            textStyle.normal.textColor = Color.white;

            Vector3 textAllignment = Vector3.down * 0.4f + Vector3.right * 0.4f;
            Handles.Label(wayPoint.waypointList[i].position + textAllignment, $"{i + 1}", textStyle);

            EditorGUI.EndChangeCheck();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Moving Handle");
                wayPoint.waypointList[i].position = handleWayPoint;
            }
        }
    }
}
