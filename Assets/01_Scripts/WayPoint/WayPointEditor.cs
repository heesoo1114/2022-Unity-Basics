using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WayPoint))] // waypoint에 대해서 custom
public class WayPointEditor : Editor
{
    // Editor 마다 target이 하나는 무조건 필요함

    WayPoint _waypoint => target as WayPoint;

    private void OnSceneGUI()
    {
        for(int i = 0; i < _waypoint.Points.Length; i++)
        {
            // create handle
            Vector3 currentWaypointPoint = _waypoint.CurrentPosition + _waypoint.Points[i];
            Vector3 newWaypointPoint = Handles.FreeMoveHandle(currentWaypointPoint, Quaternion.identity, 0.7f,
                new Vector3(x: 0.3f, y: 0.3f, z: 0.3f), Handles.SphereHandleCap);

            // numbering handles
            GUIStyle numStyle = new GUIStyle();
            numStyle.fontSize = 16;
            numStyle.fontStyle = FontStyle.Bold;
            numStyle.normal.textColor = Color.magenta;
            Vector3 textAllingnment = Vector3.down * 0.35f + Vector3.right * 0.35f;

            Handles.Label(_waypoint.CurrentPosition + _waypoint.Points[i] + textAllingnment, text: $"{i + 1}", numStyle);
        }
    }
}
