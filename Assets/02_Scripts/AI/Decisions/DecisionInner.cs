using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionInner : AIDecision
{

    [SerializeField]
    [Range(0.1f, 30f)]
    private float _distance = 5f;

    public override bool MakeADecision()
    {
        float calc = Vector3.Distance(_brain.Target.position, transform.position);

        if(calc < _distance) // 적이 시야거리내에  존재한다면
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
        if(UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _distance);
            Gizmos.color = Color.white;
        }
    }
#endif

}
