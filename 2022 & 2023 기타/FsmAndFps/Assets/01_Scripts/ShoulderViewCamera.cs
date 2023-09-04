using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ShoulderViewCamera : MonoBehaviour
{
    public Vector3 pivotOffset = new Vector3(0.0f, 1.0f, 0.0f);
    public Vector3 directOffset = new Vector3(0.4f, 0.5f, -2.0f);

    #region PUBLIC

    [Header("카메라 반응 속도")]
    public float smooth = 10f;
    public float horizontalAimingSpd = 6.0f;
    public float verticalAimingSpd = 6.0f;

    [Header("카메라 최소, 최대각")]
    public float verticalAngleMin = -60.0f;
    public float recoilAngleBounce = 5.0f;
    public float verticalAngleMax = 30.0f;

    [Header("마우스 이동에 따른 카메라 이동값")]
    private float horizontalAngle = 0.0f;
    public float GetHorizontal => horizontalAngle;
    private float verticalAngle = 0.0f;

    #endregion

    #region PRIVATE

    public Transform playerTransform;  // 플레이어 위치
    private Transform cameraTransform; // 카메라 위치
    private Camera myCamera;           // 카메라

    // 카메라와 플레이어 간의 이중체크를 하기 위한 방향과 길이
    private Vector3 realCameraPosition;  // 방향
    private float realCameraPositionMag; // 길이

    private Vector3 smoothPivotOffset;
    private Vector3 smoothCameraOffset;

    // 따라가는... (멀미 방지책)
    private Vector3 targetPivotOffset;
    private Vector3 targetDirectOffset;

    // 기본 시야각과 런닝뷰
    private float defaultFOV; 
    private float targetFOV;

    private float targetMaxVerticleAngle; // 최대 각도
    private float recoilAngle = 0f;       // 반동 각도

    #endregion

    private void Awake()
    {
        // 카메라 캐싱
        cameraTransform = transform;
        myCamera = cameraTransform.GetComponent<Camera>();

        // 카메라 높이 = 플레이어 높이 + 피봇 높이
        cameraTransform.position = playerTransform.position
            + Quaternion.identity * pivotOffset
            + Quaternion.identity * directOffset;
        cameraTransform.rotation = Quaternion.identity;

        // 방향과 길이 계산
        realCameraPosition = cameraTransform.position - playerTransform.position;
        realCameraPositionMag = realCameraPosition.magnitude - 0.5f; // 오브젝트 크기 빼주기

        // 모든 위치 초기값
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

    // 반동이후 초기 최대각으로 리셋
    public void ResetMaxVerticalAngle()
    {
        targetMaxVerticleAngle = verticalAngleMax;
    }

    #endregion

    // 각 도를 준 만큼 바운스
    public void BounceVertical(float degree)
    {
        recoilAngle = degree;
    }

    // 카메라 위치 조정 : aim < - > normal
    public void SetTargetOffset(Vector3 newPivotOffset, Vector3 newDirectOffset)
    {
        targetPivotOffset = newPivotOffset;
        targetDirectOffset = newDirectOffset;
    }

    // FOV 조정
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

        // 카메라 회전
        Quaternion camYRotation = Quaternion.Euler(0.0f, horizontalAngle, 0.0f);
        Quaternion aimRotation = Quaternion.Euler(-verticalAngle, horizontalAngle, 0.0f);
        cameraTransform.rotation = aimRotation;

        myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, targetFOV, Time.deltaTime);

        Vector3 baseTempPosition = playerTransform.position + camYRotation * targetPivotOffset;

        // 충돌하지 않을 때 offset
        Vector3 noCollisionOffset = targetDirectOffset;

        // 타겟까지 거리만큼 반복해서 구체크로 이중체크
        for (float zOffset = targetDirectOffset.z; zOffset <= 0; zOffset += .5f)
        {
            noCollisionOffset.z = zOffset;
            Vector3 vecCkPos = baseTempPosition + aimRotation * noCollisionOffset;

            if (DoubleViewingPosCheck(vecCkPos, Mathf.Abs(zOffset)) || zOffset == 0f) break;
        }

        smoothPivotOffset = Vector3.Lerp(smoothPivotOffset, targetPivotOffset, smooth * Time.deltaTime);
        smoothCameraOffset = Vector3.Lerp(smoothCameraOffset, noCollisionOffset, smooth * Time.deltaTime);

        // 카메라 포지션을 장애물 충돌 시 장애물 앞으로 이동
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
