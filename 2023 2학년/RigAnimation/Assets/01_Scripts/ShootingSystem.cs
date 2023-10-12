using Cinemachine;
using DG.Tweening;
using System;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private CinemachineFreeLook _freeLookCam;
    [SerializeField] private Transform _parentTrm;
    [SerializeField] private Transform _splatGunNozzle;

    private PlayerMovement _movement;
    private CinemachineImpulseSource _impulseSource;

    public bool _isPressFire = false;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _impulseSource = _freeLookCam.GetComponent<CinemachineImpulseSource>();
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
            Vector3 forward = -_parentTrm.forward;
            Vector3 localPos = _parentTrm.localPosition;

            _parentTrm.DOLocalMove(localPos - new Vector3(0, 0, 0.2f), 0.03f)
                .OnComplete(() => _parentTrm.DOLocalMove(localPos, 0.1f).SetEase(Ease.OutSine));

            _impulseSource.GenerateImpulse(0.3f);
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

        //여기에 총알발사 로직이 실제로 들어가야 하는데 우린 안만들꺼다.

        float camYAxis = 0;
        if (_isPressFire)
        {
            camYAxis = RemapCamera(_freeLookCam.m_YAxis.Value, 0, 1, -25, 25);
        }

        _parentTrm.localEulerAngles = new Vector3(
            Mathf.LerpAngle(_parentTrm.localEulerAngles.x, camYAxis, 0.3f),
            angle.y,
            angle.z);

    }

    private float RemapCamera(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}