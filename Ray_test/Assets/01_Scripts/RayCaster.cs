using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    [SerializeField]
    private float _maxDistance;

    private void OnDrawGizmos()
    {
        RaycastHit hit;

        // bool isHit = Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance);
        // ��𿡼� ����, ��� �������� ����, �浹�ߴٸ� ���� ���� ��� ��������, ��� ���̷� ����

        bool isHit = Physics.BoxCast(transform.position, transform.lossyScale * 0.5f, transform.forward, 
                                     out hit, transform.rotation, _maxDistance);

        if (isHit)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);

            // ������ ���ʿ� �ڽ� �׷���
            Gizmos.DrawCube(transform.position + transform.forward * hit.distance, transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * _maxDistance);
        }
    }
}
