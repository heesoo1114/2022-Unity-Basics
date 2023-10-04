using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationValue = 360;

    private void Update()
    {
        transform.Rotate(rotationValue * Time.deltaTime, 0, 0);        
    }
}
