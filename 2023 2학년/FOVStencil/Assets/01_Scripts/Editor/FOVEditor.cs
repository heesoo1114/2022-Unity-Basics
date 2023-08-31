using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerFOV))]
public class FOVEditor : Editor
{
    private void OnSceneGUI()
    {
        var pFov = (PlayerFOV)target;
        var pos = pFov.transform.position;
        Handles.color = Color.white;
        Handles.DrawWireArc(pos, Vector3.up, Vector3.forward, 360f, pFov.viewRadius);
    }
}