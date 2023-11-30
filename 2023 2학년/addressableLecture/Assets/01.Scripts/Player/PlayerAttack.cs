using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private UziGun _gun;
    [SerializeField] private float _armimgDelay = 0.2f;
    private PlayerAnimation _playerAnimation;
    private bool _isArmed = false;
    private bool _canReadyToFire;
    private Coroutine _armingCoroutine;

    private void Awake()
    {
        _playerAnimation = GetComponent<PlayerAnimation>();
        _inputReader.ArmedEvent += OnHandleArm;
        _inputReader.FireEvent += OnHandleFire;
    }

    private void OnHandleFire(bool value)
    {
        if (_isArmed && _canReadyToFire)
        {
            _gun.onFireHandle(value);
        }
    }

    private void OnDestroy()
    {
        _inputReader.ArmedEvent -= OnHandleArm;
        _inputReader.FireEvent -= OnHandleFire;
    }

    private void OnHandleArm(bool value)
    {
        _isArmed = value;
        _playerAnimation.SetArmed(_isArmed);

        if (_isArmed)
        {
            _armingCoroutine = StartCoroutine(ArmDelay());
        }
        else
        {
            StopCoroutine(_armingCoroutine);
            _canReadyToFire = false;
        }
    }

    private IEnumerator ArmDelay()
    {
        yield return new WaitForSeconds(_armimgDelay);
        _canReadyToFire = true;
    }
}
