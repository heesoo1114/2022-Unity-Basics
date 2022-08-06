using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IHitable, IAgent
{
    public bool IsEnemy => false;

    public Vector3 HitPoint { get; private set; }

    #region 체력관련 부분
    [SerializeField]
    private int _maxHealth;
    private int _health;
    public int Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
            OnUpdateHealthUI?.Invoke(_health);
        }
    }
    #endregion
    private bool _isDead = false;

    [field: SerializeField] public UnityEvent OnDie { get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    [field: SerializeField] public UnityEvent<int> OnUpdateHealthUI { get; set; }

    private AgentMovement _agentMovement;

    private void Awake()
    {
        _agentMovement = GetComponent<AgentMovement>();
        Health = _maxHealth;
    }

    public void GetHit(int damage, GameObject damageDealer)
    {
        if (_isDead) return;
        Health -= damage;
        OnUpdateHealthUI?.Invoke(Health);
        OnGetHit?.Invoke();
        if(Health <= 0)
        {
            OnDie?.Invoke();
            _isDead = true;
        }
    }

}
