using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageAble
{
    public UnityEvent OnHitTriggered = null;
    public UnityEvent OnDeadTriggered = null;

    private AIActionData _aiActionData;

    public Action<int, int> OnHealthChanged = null;

    public bool IsDead { get; set; }

    private int _maxHP;
    private int _currentHP;

    public int MaxHP => _maxHP;
    public int CurrentHP => _currentHP; 

    private void Awake()
    {
        _aiActionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public void SetMaxHP(int value)
    {
        _currentHP = _maxHP = value;
        IsDead = false;
    }

    public void OnDamage(int damage, Vector3 point, Vector3 normal)
    {
        if (IsDead) return;

        _aiActionData.HitPoint = point;
        _aiActionData.HitNormal = normal;

        // 실질적인 체력 감소는 여기서
        _currentHP -= damage;
        _currentHP = Mathf.Clamp(_currentHP, 0, _maxHP);

        if (_currentHP <= 0)
        {
            IsDead = true;
            OnDeadTriggered?.Invoke();
        }

        OnHitTriggered?.Invoke();

        UIManager.Instance.Subscribe(this); // 구독

        OnHealthChanged?.Invoke(_currentHP, _maxHP); // 전파
    }
}
