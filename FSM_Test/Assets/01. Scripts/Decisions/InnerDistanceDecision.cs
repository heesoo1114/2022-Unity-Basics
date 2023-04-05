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

    public override bool MakeDecision()
    {
        return Vector3.Distance(_playerTrm.position, transform.position) < _range; // ��Ÿ� �ȿ� ���� �ִ���
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _range);
        Gizmos.color = Color.white;
    }
#endif
}
