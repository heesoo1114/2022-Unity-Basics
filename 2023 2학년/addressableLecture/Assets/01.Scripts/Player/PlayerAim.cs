using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [Header("참조 변수들")]
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _visualTrm;
    private PlayerMovement _playerMovement;
    private PlayerAnimation _playerAnimation;

    [Header("셋팅값")]
    [SerializeField] private LayerMask _whatIsGround;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerAnimation = GetComponent<PlayerAnimation>();
    }

    private void LateUpdate()
    {
        Vector2 mousePos = _inputReader.AimPosition;
        Ray ray = CameraManager.MainCam.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, CameraManager.MainCam.farClipPlane, _whatIsGround))
        {
            Vector3 worldMousePos = hitInfo.point;
            Vector3 dir = (worldMousePos - transform.position);
            dir.y = 0;
            _visualTrm.rotation = Quaternion.LookRotation(dir);
        }

        CalculateRotation();
    }

    private void CalculateRotation()
    {
        Vector3 forwardVector = Quaternion.Inverse(_visualTrm.rotation) * _playerMovement.MovementVelocity;
        Vector2 dir = new Vector2(forwardVector.x, forwardVector.z).normalized;
        _playerAnimation.SetAnimationDirection(dir);
    }

}