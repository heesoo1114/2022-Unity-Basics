using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageAble
{
    public UnityEvent OnHitTriggered = null;
    private AIActionData _aiActionData;

    private void Awake()
    {
        _aiActionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public void OnDamage(int damage, Vector3 point, Vector3 normal)
    {
        _aiActionData.HitPoint = point;
        _aiActionData.HitNormal = normal;

        OnHitTriggered?.Invoke();
    }
}
