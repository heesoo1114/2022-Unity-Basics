using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    CarMovement _carMovement;

    private float carSpeed;

    private void Awake()
    {
        _carMovement = transform.parent.GetComponent<CarMovement>();
    }

    private void Update()
    {
        carSpeed = _carMovement.MoveSpeed;
    }
}
