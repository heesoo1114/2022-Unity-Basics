/*using Cinemachine;
using System;
using UnityEngine;

public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}

[Serializable]
public class CustomInspectorObjects
{
    public bool swapCameras = false;
    public bool panCameraOnContact = false;

    [HideInInspector] public CinemachineVirtualCamera cameraOnLeft;
    [HideInInspector] public CinemachineVirtualCamera cameraOnRight;

    [HideInInspector] public PanDirection panDirection;
    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = 0.35f;
}

public class CameraControlTrigger : MonoBehaviour
{
    public CustomInspectorObjects customInspectorObject;

    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            var obj = customInspectorObject;
            if (obj.panCameraOnContact)
            {
                CameraManager.Instance.PanCameraOnContact(obj.panDistance, obj.panTime, obj.panDirection, false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            var obj = customInspectorObject;
            if (obj.panCameraOnContact)
            {
                CameraManager.Instance.PanCameraOnContact(obj.panDistance, obj.panTime, obj.panDirection, true);
            }

        }

    }
}*/