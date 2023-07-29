using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class InnerDistanceDecision : AIDecsion
{
    [SerializeField]
    private Transform _playerTrm;

    [SerializeField]
    private float _range;

    [SerializeField]
    private bool _alwaysVisible;

    public override bool MakeDecision()
    {
        bool isIn = Vector3.Distance(_playerTrm.position, transform.position) < _range; // 사거리 안에 들어와 있는지

        if (isIn)
        {
            _actionData.LastSpotPos = _playerTrm.position;
        }
        return isIn;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject || _alwaysVisible)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _range);
            Gizmos.color = Color.white;
        }
    }
#endif
}
