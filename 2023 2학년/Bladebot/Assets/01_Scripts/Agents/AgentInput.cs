using System;
using UnityEngine;
using UnityEngine.VFX;
using static Core.Define;

public class AgentInput : MonoBehaviour
{
    [SerializeField]
    private LayerMask _whatIsGround;

    public event Action<Vector3> OnMovementKeyPress = null;
    public event Action OnAttackKeyPress = null;
    public event Action OnRollingKeyPress = null; // 회피키 이벤트

    private Vector3 _directionInput;

    private void Update()
    {
        UpdateMoveIpnut();
        UpdateAttackInput();
        UpdateRollingInput();
    }

    private void UpdateRollingInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            OnRollingKeyPress?.Invoke();
        }
    }

    private void UpdateAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnAttackKeyPress?.Invoke();
        }
    }

    private void UpdateMoveIpnut()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        _directionInput = new Vector3(h, 0, v);
        
        OnMovementKeyPress?.Invoke(_directionInput);
    }

    public Vector3 GetCurrentInputDirection()
    {
        return Quaternion.Euler(0, -45f, 0) * _directionInput.normalized;
    }

    public Vector3 GetMouseWorldPosision()
    {
        Ray ray = MainCam.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        bool result = Physics.Raycast(ray, out hit, MainCam.farClipPlane, _whatIsGround);
        if (result)
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
