using UnityEngine;

public class EnemyUIBillboard : MonoBehaviour
{
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void LateUpdate()
    {
        Vector3 cameraRotation = _cam.transform.rotation * Vector3.forward;
        Vector3 posTarget = transform.position + cameraRotation;
        Vector3 olrientationTarget = _cam.transform.rotation * Vector3.forward;
        transform.LookAt(posTarget);
    }
}
