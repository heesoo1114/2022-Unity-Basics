using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static Action OnEnemyKilled;

    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Transform barPosition;

    [SerializeField] private float initialHealth = 10f;
    [SerializeField] private float maxHealth = 10f;

    public float CurrentHelath { get; set; }

    private Image _healthBar;

    private void Start()
    {
        CreateHealthBar();
        CurrentHelath = initialHealth;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            DealDamage(5f);
        }

        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, CurrentHelath/maxHealth, Time.deltaTime * 10f);
    }

    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(transform);

        EnemyHealthContainer container  = newBar.GetComponent<EnemyHealthContainer>();
        _healthBar = container.FillAmountImage;
    }

    public void DealDamage(float damageReceived)
    {
        CurrentHelath -= damageReceived;

        if(CurrentHelath <= 0)
        {
            CurrentHelath = 0;
            // Die;
            Die();
        }
    }

    public void ResetHealth()
    {
        CurrentHelath = initialHealth;
        _healthBar.fillAmount = 1;
    }

    private void Die()
    {
        /*CurrentHelath = initialHealth;
        _healthBar.fillAmount = 1;*/

        ResetHealth();

        OnEnemyKilled?.Invoke();

        ObjPooler.ReturnToPool(gameObject);
    }
}
