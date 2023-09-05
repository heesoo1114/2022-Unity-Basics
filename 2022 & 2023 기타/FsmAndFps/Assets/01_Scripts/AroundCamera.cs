using UnityEngine;

public class AroundCamera : MonoBehaviour
{
    public Transform target = null;

    public float speedRoation;

    private void LateUpdate()
    {
        transform.RotateAround(target.position, Vector3.up, speedRoation * Time.deltaTime);
        transform.LookAt(target);
    }
}
