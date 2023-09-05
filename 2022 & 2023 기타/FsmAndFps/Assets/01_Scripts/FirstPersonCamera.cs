using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform firstCamTarget = null;

    public float spdMouseX = 5.0f;
    public float spdMouseY = 5.0f;

    public float rotationMouseX = 0.0f;
    public float rotationMouseY = 0.0f;

    private void FirstCam()
    {
        float mouseX = Input.GetAxis("Mouse X");
        rotationMouseX = transform.localEulerAngles.x + mouseX * spdMouseX;
        rotationMouseX = (rotationMouseX > 180.0f) ? rotationMouseX - 360.0f : rotationMouseX;
        
        float mouseY = Input.GetAxis("Mouse Y");
        rotationMouseY = transform.localEulerAngles.y + mouseY * spdMouseY;
        rotationMouseY = (rotationMouseY > 180.0f) ? rotationMouseY - 360.0f : rotationMouseY;

        transform.localEulerAngles = new Vector3(rotationMouseY, rotationMouseX, 0.0f);
        transform.position = firstCamTarget.position;
    }

    private void LateUpdate()
    {
        FirstCam();
    }
}
