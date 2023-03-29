using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.AI;

public class OffMeshClimb : MonoBehaviour
{
    [SerializeField]
    private int _offMeshArea = 4; // �����޽��� ������ ����

    [SerializeField]
    private float _climbSpeed = 1.5f;
    private NavMeshAgent _navAgent;

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    IEnumerator Start() // = start���� startcoroutine�� ����
    {
        while (true)
        {
            yield return new WaitUntil(() => IsOnClimb());
            yield return StartCoroutine(ClimbOrDescend());
        }
    }

    private bool IsOnClimb()
    {
        if (_navAgent.isOnOffMeshLink) // ���� �޽ø�ũ �� ������Ʈ�� �ö�� �ֳ�
        {
            OffMeshLinkData linkData = _navAgent.currentOffMeshLinkData;

            if (linkData.offMeshLink != null && linkData.offMeshLink.area == _offMeshArea)
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator ClimbOrDescend()
    {
        _navAgent.isStopped = true;
        OffMeshLinkData linkData = _navAgent.currentOffMeshLinkData;
        Vector3 start = linkData.startPos;
        Vector3 end = linkData.endPos;

        float climbTime = Mathf.Abs(end.y - start.y) / _climbSpeed;
        // ������ �ӵ��� �� �Ÿ��� �����ٰ� ������ �� �� �ʰ� �ɸ���

        float currentTime = 0;
        float percent = 0;
        
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / climbTime;
            transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        _navAgent.CompleteOffMeshLink(); // ���¸޽ø�ũ�� Ż�������� �˸�
        _navAgent.isStopped = false; // ������ �簳
    }
}
