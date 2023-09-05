using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FollowCamera))]
public class CameraWithEditor : Editor
{
    private FollowCamera followCamera;

    // ������ obj�� �ν����Ϳ��� ���õ� �� ����Ǵ� �Լ�
    public override void OnInspectorGUI()
    {
        followCamera = (FollowCamera)target;
        base.OnInspectorGUI();
    }

    // ������ obj�� scene vivew�� ǥ�õ� �� ����Ǵ� �Լ�
    private void OnSceneGUI()
    {
        if (!followCamera || !followCamera.camTarget)
            return;

        Transform camTarget = followCamera.camTarget;
        Vector3 posTarget = camTarget.position;
        posTarget.y += followCamera.targetHeight;

        Handles.color = Color.red;
        Handles.DrawSolidDisc(posTarget, Vector3.up, followCamera.camDistance);

        Handles.color = Color.green;
        Handles.DrawSolidDisc(posTarget, Vector3.up, followCamera.camDistance);


        followCamera.camDistance = Handles.ScaleSlider(
            followCamera.camDistance, posTarget, -camTarget.forward, Quaternion.identity, followCamera.camDistance, 0.1f);

        followCamera.camDistance = Mathf.Clamp(
            followCamera.camDistance, 2f, float.MaxValue);

        Handles.color = Color.yellow;
        followCamera.camHeight = Handles.ScaleSlider(
            followCamera.camHeight, posTarget, Vector3.up, Quaternion.identity, followCamera.camHeight, 0.1f);

        followCamera.camHeight = Mathf.Clamp(
            followCamera.camHeight, 2f, float.MaxValue);


        GUIStyle gUIStyleLabel = new GUIStyle();
        gUIStyleLabel.fontSize = 15;
        gUIStyleLabel.normal.textColor = Color.white;

        gUIStyleLabel.alignment = TextAnchor.UpperCenter;
        Handles.Label(
                        posTarget + (-camTarget.forward * followCamera.camDistance),
                        "Distance",
                        gUIStyleLabel
            );

        gUIStyleLabel.alignment = TextAnchor.MiddleRight;
        Handles.Label(
                        posTarget + (Vector3.up * followCamera.camHeight),
                        "Height",
                        gUIStyleLabel
            );

        followCamera.TopDownFollowCamera();
    }
}
