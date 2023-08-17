using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsGround;

    private Camera _mainCam;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        _mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        Vector2 mousePos = _playerInput.AimPosition;
        Ray ray = _mainCam.ScreenPointToRay(mousePos);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, _mainCam.farClipPlane, _whatIsGround))
        {
            Vector3 worldMousePos = hitInfo.point;
            Vector3 dir = (worldMousePos - transform.position);
            dir.y = 0;
            transform.rotation = Quaternion.LookRotation(dir);
        }

    }

}