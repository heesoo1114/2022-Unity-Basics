using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AIBrain))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        AIBrain brain = target as AIBrain;
        float rad = -brain.viewAngle * 0.5f * Mathf.Deg2Rad;
        Vector3 angle = new Vector3(Mathf.Sin(rad), Mathf.Cos(rad), 0);

        Handles.color = Color.white;
        Handles.DrawWireDisc(brain.transform.position, Vector3.forward, brain.viewRange);

        Handles.color = new Color(1, 1, 1, 0.5f);
        Handles.DrawSolidArc(brain.transform.position,
            Vector3.forward * -1,
            angle,
            brain.viewAngle,
            brain.viewRange);

        GUIStyle style = new GUIStyle();
        style.fontSize = 35;
        Handles.Label(
            brain.transform.position + brain.transform.up * 2f,
            brain.viewAngle.ToString(), style);
    }
}
