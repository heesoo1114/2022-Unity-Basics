/*using Cinemachine;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraControlTrigger))]
public class CameraControlEditor : Editor
{
    private CameraControlTrigger _cameraControlTrigger;

    private void OnEnable()
    {
        _cameraControlTrigger = (CameraControlTrigger)target; //타겟 가져오고
    }


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CustomInspectorObjects inspectorObj = _cameraControlTrigger.customInspectorObject;
        if (inspectorObj.swapCameras)
        {
            //https://docs.unity3d.com/ScriptReference/EditorGUILayout.ObjectField.html
            // allowSceneObject => 씬에 있는 애들 드래그앤드롭 가능하도록 할거냐.
            inspectorObj.cameraOnLeft
                = EditorGUILayout.ObjectField("Camera on Left",
                            inspectorObj.cameraOnLeft,
                            typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
            inspectorObj.cameraOnRight
                = EditorGUILayout.ObjectField("Camera on Right",
                            inspectorObj.cameraOnRight,
                            typeof(CinemachineVirtualCamera), true) as CinemachineVirtualCamera;
        }

        if (inspectorObj.panCameraOnContact)
        {
            inspectorObj.panDirection = (PanDirection)EditorGUILayout.EnumPopup("Camera Pan Direction",
                inspectorObj.panDirection);

            inspectorObj.panDistance = EditorGUILayout.FloatField("Pan Distance", inspectorObj.panDistance);
            inspectorObj.panTime = EditorGUILayout.FloatField("Pan Time", inspectorObj.panTime);
        }

        //GUI상에서 변경이 감지되었다면 새로 그리길 명령.
        if (GUI.changed)
        {
            EditorUtility.SetDirty(_cameraControlTrigger);
        }
    }

}*/