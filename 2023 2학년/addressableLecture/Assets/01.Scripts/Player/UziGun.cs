using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UziGun : MonoBehaviour
{
    [SerializeField] private Transform _firePos;

    [SerializeField] private float _shootRange = 10f;
    [SerializeField] private float _coolTime = 0.2f;

    public UnityEvent OnFire;

    private float _lastFireTime;
    private bool _isFire = false;

    public Vector3 HitPoint { get; private set; }

    public void onFireHandle(bool value)
    {
        _isFire = value;
    }

    private void Update()
    {
        bool canFire = _isFire && _lastFireTime + _coolTime < Time.deltaTime;

        if (canFire)
        {
            if (Physics.Raycast(_firePos.position, _firePos.forward, out RaycastHit hitInfo, _shootRange))
            {
                HitPoint = hitInfo.point;
                OnFire?.Invoke();
            }
            else
            {
                HitPoint = _firePos.position + _firePos.forward * _shootRange;
                OnFire?.Invoke();
            }
            _lastFireTime = Time.time;
        }
    }
}