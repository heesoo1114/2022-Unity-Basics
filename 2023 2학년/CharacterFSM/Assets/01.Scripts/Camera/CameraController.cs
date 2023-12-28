using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera[] _virtualCameras;

    [Header("lerp for jump and fall")]
    [SerializeField] private float _fallPanAmount = 0.25f;
    [SerializeField] private float _fallYPanTime = 0.35f;
    public float fallSpeedYDampingChangeThreshold = -15f;

    public bool IsLerpingYDamping { get; private set; }
    public bool LerpedFromPlayerFalling { get; set; }

    private Tween _lerpYPanTween;
    private CinemachineFramingTransposer _framingTransposer;
    private CinemachineVirtualCamera _currentCam;

    private float _normalYPanAmount;

    private void Awake()
    {
        ChangeCamera(_virtualCameras[0]);
    }

    public void ChangeCamera(CinemachineVirtualCamera activeCam)
    {
        for (int i = 0; i < _virtualCameras.Length; ++i)
        {
            _virtualCameras[i].Priority = 5;
        }

        activeCam.Priority = 10;
        _currentCam = activeCam;
        _framingTransposer = _currentCam.GetCinemachineComponent<CinemachineFramingTransposer>();
        _normalYPanAmount = _framingTransposer.m_YDamping;
    }


    public void LerpYDamping(bool isPlayerFalling)
    {
        if (_lerpYPanTween != null && _lerpYPanTween.IsActive())
            _lerpYPanTween.Kill();

        float endDamingAmount = _normalYPanAmount;
        if (isPlayerFalling)
        {
            endDamingAmount = _fallPanAmount;
            LerpedFromPlayerFalling = true;
        }

        //y뎀핑값을 서서히 줄이거나 키워주는 역할을 하는 트윈.
        IsLerpingYDamping = true;
        _lerpYPanTween = DOTween.To(
                () => _framingTransposer.m_YDamping,
                value => _framingTransposer.m_YDamping = value,
                endDamingAmount,
                _fallYPanTime)
            .OnComplete(() => IsLerpingYDamping = true);
    }

}
