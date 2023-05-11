using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private UIDocument _document;
    private VisualElement _root;
    private EnemyHPBar _enemyBar;

    [SerializeField]
    private float _enemyBarTimer = 4f;
    private float _currentEnemyBarTimer = 0;

    private EnemyHealth _subscribedEnemy = null;

    private void Awake()
    {
        _document = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _document.rootVisualElement;

        VisualElement _hpBarRoot = _root.Q<VisualElement>("BarRect");
        _enemyBar = new EnemyHPBar(_hpBarRoot);
    }

    public void Subscribe(EnemyHealth health)
    {
        if (_currentEnemyBarTimer <= 0)
        {
            _enemyBar.ShowBar(true);
        }

        if (_subscribedEnemy != health)
        {
            if (_subscribedEnemy != null)
            {
                _subscribedEnemy.OnHealthChanged -= UpdateEnemyHPData;
            }

            _subscribedEnemy = health;
            _subscribedEnemy.OnHealthChanged += UpdateEnemyHPData;

            _enemyBar.EnemyName = health.gameObject.name;
            _enemyBar.MaxHP = _subscribedEnemy.MaxHP;
        }
    }

    private void UpdateEnemyHPData(int current, int max)
    {
        _enemyBar.HP = current;
        _currentEnemyBarTimer = _enemyBarTimer; // °»½Å
    }

    private void Update()
    {
        if (_currentEnemyBarTimer > 0)
        {
            _currentEnemyBarTimer -= Time.deltaTime;
            if (_currentEnemyBarTimer <= 0)
            {
                _enemyBar.ShowBar(false);
            }
        }
    }
}