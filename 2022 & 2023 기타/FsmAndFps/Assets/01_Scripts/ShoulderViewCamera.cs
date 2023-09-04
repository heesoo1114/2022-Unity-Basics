using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ShoulderViewCamera : MonoBehaviour
{
    public Vector3 pivotOffset = new Vector3(0.0f, 1.0f, 0.0f);
    public Vector3 directOffset = new Vector3(0.4f, 0.5f, -2.0f);

    #region PUBLIC

    [Header("ī�޶� ���� �ӵ�")]
    public float smooth = 10f;
    public float horizontalAimingSpd = 6.0f;
    public float verticalAimingSpd = 6.0f;

    [Header("ī�޶� �ּ�, �ִ밢")]
    public float verticalAngleMin = -60.0f;
    public float recoilAngleBounce = 5.0f;
    public float verticalAngleMax = 30.0f;

    [Header("���콺 �̵��� ���� ī�޶� �̵���")]
    private float horizontalAngle = 0.0f;
    public float GetHorizontal => horizontalAngle;
    private float verticalAngle = 0.0f;

    #endregion

    #region PRIVATE

    public Transform playerTransform;  // �÷��̾� ��ġ
    private Transform cameraTransform; // ī�޶� ��ġ
    private Camera myCamera;           // ī�޶�

    // ī�޶�� �÷��̾� ���� ����üũ�� �ϱ� ���� ����� ����
    private Vector3 realCameraPosition;  // ����
    private float realCameraPositionMag; // ����

    private Vector3 smoothPivotOffset;
    private Vector3 smoothCameraOffset;

    // ���󰡴�... (�ֹ� ����å)
    private Vector3 targetPivotOffset;
    private Vector3 targetDirectOffset;

    // �⺻ �þ߰��� ���׺�
    private float defaultFOV; 
    private float targetFOV;

    private float targetMaxVerticleAngle; // �ִ� ����
    private float recoilAngle = 0f;       // �ݵ� ����

    #endregion

    private void Awake()
    {
        // ī�޶� ĳ��
        cameraTransform = transform;
        myCamera = cameraTransform.GetComponent<Camera>();

        // ī�޶� ���� = �÷��̾� ���� + �Ǻ� ����
        cameraTransform.position = playerTransform.position
            + Quaternion.identity * pivotOffset
            + Quaternion.identity * directOffset;
        cameraTransform.rotation = Quaternion.identity;

        // ����� ���� ���
        realCameraPosition = cameraTransform.position - playerTransform.position;
        realCameraPositionMag = realCameraPosition.magnitude - 0.5f; // ������Ʈ ũ�� ���ֱ�

        // ��� ��ġ �ʱⰪ
        smoothPivotOffset = pivotOffset;
        smoothCameraOffset = directOffset;
        defaultFOV = myCamera.fieldOfView;
        horizontalAngle = playerTransform.eulerAngles.y;

        ResetTargetOffsets();
        ResetFOV();
        ResetMaxVerticalAngle();
    }

    #region RESET

    public void ResetTargetOffsets()
    {
        targetPivotOffset = pivotOffset;
        targetDirectOffset = directOffset;
    }

    public void ResetFOV()
    {
        targetFOV = defaultFOV;
    }

    // �ݵ����� �ʱ� �ִ밢���� ����
    public void ResetMaxVerticalAngle()
    {
        targetMaxVerticleAngle = verticalAngleMax;
    }

    #endregion

    // �� ���� �� ��ŭ �ٿ
    public void BounceVertical(float degree)
    {
        recoilAngle = degree;
    }

    // ī�޶� ��ġ ���� : aim < - > normal
    public void SetTargetOffset(Vector3 newPivotOffset, Vector3 newDirectOffset)
    {
        targetPivotOffset = newPivotOffset;
        targetDirectOffset = newDirectOffset;
    }

    // FOV ����
    public void SetFOV(float customFOV)
    {
        targetFOV = customFOV;
    }

    #region CHECK

    private bool ViewingPosCheck(Vector3 ckPosition, float deltaPlayerHeight)
    {
        Vector3 target = playerTransform.position + (Vector3.up * deltaPlayerHeight);

        if (Physics.SphereCast(ckPosition, 0.2f, target - ckPosition, out RaycastHit hit, realCameraPositionMag))
        {
            if (hit.transform != playerTransform && !hit.transform.GetComponent<Collider>().isTrigger)
            {
                return false;
            }
        }
        return true;
    }

    private bool ReverseViewingPosCk(Vector3 ckPosition, float deltaPlayerHeight, float maxDistance)
    {
        Vector3 origin = playerTransform.position + (Vector3.up * deltaPlayerHeight);

        if (Physics.SphereCast(origin, 0.2f, ckPosition - origin, out RaycastHit hit, maxDistance))
        {
            if (hit.transform != playerTransform && hit.transform != transform && !hit.transform.GetComponent<Collider>().isTrigger)
            {
                return false;
            }
        }
        return true;
    }

    private bool DoubleViewingPosCheck(Vector3 checkPosition, float offset)
    {
        float playerFocusHeight = playerTransform.GetComponent<CapsuleCollider>().height * 0.75f;
        return ViewingPosCheck(checkPosition, playerFocusHeight) && ReverseViewingPosCk(checkPosition, playerFocusHeight, offset);
    }

    #endregion

    private void Update()
    {
        float mouseX = Mathf.Clamp(Input.GetAxis("Mouse X"), -1f, 1f);
        horizontalAngle += mouseX * horizontalAimingSpd;

        float mouseY = Mathf.Clamp(Input.GetAxis("Mouse Y"), -1f, 1f);
        verticalAngle += mouseY * verticalAimingSpd;

        verticalAngle = Mathf.Clamp(verticalAngle, verticalAngleMin, targetMaxVerticleAngle);
        verticalAngle = Mathf.LerpAngle(verticalAngle, verticalAngle + recoilAngle, 10.0f * Time.deltaTime);

        // ī�޶� ȸ��
        Quaternion camYRotation = Quaternion.Euler(0.0f, horizontalAngle, 0.0f);
        Quaternion aimRotation = Quaternion.Euler(-verticalAngle, horizontalAngle, 0.0f);
        cameraTransform.rotation = aimRotation;

        myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, targetFOV, Time.deltaTime);

        Vector3 baseTempPosition = playerTransform.position + camYRotation * targetPivotOffset;

        // �浹���� ���� �� offset
        Vector3 noCollisionOffset = targetDirectOffset;

        // Ÿ�ٱ��� �Ÿ���ŭ �ݺ��ؼ� ��üũ�� ����üũ
        for (float zOffset = targetDirectOffset.z; zOffset <= 0; zOffset += .5f)
        {
            noCollisionOffset.z = zOffset;
            Vector3 vecCkPos = baseTempPosition + aimRotation * noCollisionOffset;

            if (DoubleViewingPosCheck(vecCkPos, Mathf.Abs(zOffset)) || zOffset == 0f) break;
        }

        smoothPivotOffset = Vector3.Lerp(smoothPivotOffset, targetPivotOffset, smooth * Time.deltaTime);
        smoothCameraOffset = Vector3.Lerp(smoothCameraOffset, noCollisionOffset, smooth * Time.deltaTime);

        // ī�޶� �������� ��ֹ� �浹 �� ��ֹ� ������ �̵�
        cameraTransform.position = playerTransform.position + camYRotation * smoothPivotOffset + aimRotation * smoothCameraOffset;

        if (recoilAngle > 0f)
            recoilAngle -= recoilAngleBounce * Time.deltaTime;
        else if (recoilAngle < 0f)
            recoilAngle += recoilAngleBounce * Time.deltaTime;
    }

    public float GetNowPivotMagnitude(Vector3 finalPivotOffset)
    {
        return Mathf.Abs((finalPivotOffset - smoothPivotOffset).magnitude);
    }
}
