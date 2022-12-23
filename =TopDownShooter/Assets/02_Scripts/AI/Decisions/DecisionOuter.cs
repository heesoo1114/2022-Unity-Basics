using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionOuter : AIDecision
{
    [SerializeField]
    [Range(0.1f, 30f)]
    private float _distance = 5f;
    public float Distance { get => _distance; set => _distance = Mathf.Clamp(value, 0.1f, 30f); }

    public override bool MakeADecision()
    {
        float calc = Vector2.Distance(_brain.Target.position, _brain.BasePosition.position);

        if (calc > _distance) // 적이 시야거리내에  존재한다면
        {
            return true;
        }
        else
        {
            return false;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _distance);
            Gizmos.color = Color.white;
        }
    }
#endif
}
