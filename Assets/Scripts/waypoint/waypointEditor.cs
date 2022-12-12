using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(waypoint))] //waypoint ´ëÇØ¼­ custom
public class waypointEditor : Editor
{
    waypoint waypoint => target as waypoint;

    private void OnSceneGUI()
    {
        for (int i=0; i<waypoint.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();

            //create handle
            Vector3 currentWaypointPoint = waypoint.CurrentPosition + waypoint.Points[i];
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, Quaternion.identity,
                0.7f, new Vector3(x: 0.3f, y: 0.3f, z: 0.3f), Handles.SphereHandleCap);

            //numbering handles
            GUIStyle numStyle = new GUIStyle();
            numStyle.fontSize = 16;
            numStyle.fontStyle = FontStyle.Bold;
            numStyle.normal.textColor = Color.magenta;
            Vector3 textAllignment = Vector3.down * 0.35f + Vector3.right * 0.35f;

            Handles.Label(waypoint.CurrentPosition + waypoint.Points[i] + textAllignment, text: $"{i + 1}", numStyle);

            EditorGUI.EndChangeCheck();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, name: "Free Move Handle");
                waypoint.Points[i] = newWaypointPoint-waypoint.CurrentPosition;
            }
        }
    }
}
