using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody _rigid;
    Transform _modelTr;

    [SerializeField] private Transform _startTr;
    [SerializeField] private Transform _readyTr;

    [Header("Movement")]
    public float sideSpeed;
    public float minX;
    public float maxX;

    [Header("Dash")]
    public float dashSpeed;
    public float dashDuration = 0.5f;
    private bool isDashing = false;

    [Header("Roatation")]
    public float rollAmount;
    public float lerpAmount;
    Vector3 rotateValue;

    [SerializeField] private TextMeshProUGUI _debuggingText;

    private void Awake()
    {
        _rigid = GetComponent<Rigidbody>();
        _modelTr = transform.Find("Model").GetComponent<Transform>();
    }

    private void Update()
    {
        float z = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DashMovement(z);
        }
        
        HorizontalMovement(z);
        RotationPlane(z);

        PositionClamp();
    }

    public void StartAnimation()
    {
        transform.DOMove(_startTr.transform.position, 1.5f);
    }

    public void Init()
    {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        transform.position = _readyTr.transform.position;
    }

    public void PositionClamp()
    {
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        transform.position = clampedPosition;
    }

    public void HorizontalMovement(float x)
    {
        Vector3 movement = new Vector3(x, 0f, 0f);
        _rigid.velocity = movement * sideSpeed;
    }

    public void RotationPlane(float z)
    {
        Vector3 lerpVector = new Vector3(0, 0, -z * rollAmount);
        rotateValue = Vector3.Lerp(rotateValue, lerpVector, lerpAmount * Time.deltaTime);
        _modelTr.rotation *= Quaternion.Euler(rotateValue * Time.fixedDeltaTime);
    }

    public void DashMovement(float z)
    {
        Vector3 dashDir = (z == 1) ? Vector3.right : Vector3.left;

        StartCoroutine(Dash(dashDir, z));
    }

    private IEnumerator Dash(Vector3 dashDir, float plusMinus)
    {
        isDashing = true;
        Vector3 dashDirection = dashDir;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            float dashSpeedCurrent = Mathf.Lerp(dashSpeed, 0f, elapsedTime / dashDuration);

            _rigid.velocity = dashDirection * dashSpeedCurrent;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _rigid.velocity = Vector3.zero;
        isDashing = false;
    }

    private void StopImmediatelly()
    {
        sideSpeed = 0;
        dashSpeed = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Happen");
        GameManager.Instance.GameOver();
    }

    #region Accel,Decel
    // private float AccelSpeed()
    // {
    //     moveSpeed += acceleration * Time.deltaTime;
    // 
    //     return Mathf.Clamp(moveSpeed, 0, maxSpeed);
    // }
    // 
    // private float DecelSpeed()
    // {
    //     moveSpeed -= deAcceleration * Time.deltaTime;
    // 
    //     return Mathf.Clamp(moveSpeed, 0, maxSpeed);
    // }
    #endregion
}
