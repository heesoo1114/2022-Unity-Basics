using UnityEngine;
using Cinemachine;
using System;
using DG.Tweening;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CinemachineFreeLook _freeLookCam;
    [SerializeField] private Transform _parentTrm;
    [SerializeField] private Transform _splatGunNozzle;

    private PlayerMovement _movement;
    private CinemachineImpulseSource _impurseSource;

    private bool _isPressFire = false;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _impurseSource = _freeLookCam.GetComponent<CinemachineImpulseSource>();
        _inputReader.FireEvent += OnHandleFire;
    }

    private void OnDestroy()
    {
        _inputReader.FireEvent -= OnHandleFire;
    }

    private void OnHandleFire(bool value)
    {
        _isPressFire = value;
    }

    private void VisualPolish()
    {
        if (!DOTween.IsTweening(_parentTrm))
        {
            _parentTrm.DOComplete();
            Vector3 forward = -1 * _parentTrm.forward;
            Vector3 localPos = _parentTrm.localPosition;

            _parentTrm.DOLocalMove(localPos - new Vector3(0, 0, 0.2f), 0.03f)
                .OnComplete(() => _parentTrm.DOLocalMove(localPos, 0.1f)
                .SetEase(Ease.OutSine));

            _impurseSource.GenerateImpulse(0.1f);
        }
    }

    private void Update()
    {
        Vector3 angle = _parentTrm.localEulerAngles;
        _movement.blockRotationPlayer = _isPressFire;

        if (_isPressFire)
        {
            _movement.RotateToCamera(transform);
            VisualPolish();
        }

        // 원래라면 총알 발사 로직 구현해야 하는데 만들지 않음.

        float camYAxis = 0;
        if (_isPressFire)
        {
            camYAxis = RemapCamera(_freeLookCam.m_YAxis.Value, 0, 1, -25, 25);
        }

        _parentTrm.localEulerAngles = new Vector3(
            Mathf.LerpAngle(_parentTrm.localEulerAngles.x, camYAxis, 0.3f), angle.y, angle.z);
    }

    private float RemapCamera(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
