using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private float orthographicSize;
    private float targetOrthographicSize;

    public float moveSpeed = 50f;
    public float zoomAmount = 2f;
    public float zoomSpeed = 5f;

    public float minOrthographicSize = 10f;
    public float maxOrthographicSize = 30f;

    private void Start()
    {
        orthographicSize = virtualCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }
    private void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(x, y).normalized;


        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
    private void HandleZoom()
    {
        targetOrthographicSize += -Input.mouseScrollDelta.y * zoomAmount;
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, Time.deltaTime * zoomSpeed);

        virtualCamera.m_Lens.OrthographicSize = orthographicSize;
    }
}
