using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.forward * 1, Space.World);
    }
}
