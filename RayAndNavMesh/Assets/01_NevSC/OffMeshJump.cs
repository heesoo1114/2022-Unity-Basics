using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OffMeshJump : MonoBehaviour
{
    [SerializeField]
    private float _jumpSpeed = 10.0f;
    [SerializeField]
    private float _gravity = -9.81f;
    private NavMeshAgent _navAgent;

    [SerializeField]
    private int _offMeshArea = 2;

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitUntil(() => IsOnJump());
            yield return StartCoroutine(JumpTo());
        }
    }

    private IEnumerator JumpTo()
    {
        _navAgent.isStopped = true;

        OffMeshLinkData linkdata = _navAgent.currentOffMeshLinkData;
        Vector3 start = transform.position;
        Vector3 end = linkdata.endPos;

        float jumpTime = MathF.Max(0.3f, Vector3.Distance(start, end)) / _jumpSpeed;

        float currentTime = 0;
        float percent = 0;

        float v0 = (end - start).y - _gravity; // y ������ �ְ����̰� �̰� �ӵ��� ���ǵ�

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / jumpTime; // lerp�� �ð�

            Vector3 pos = Vector3.Lerp(start, end, percent);
            // ������ �: ������ġ + �ʱ�ӵ� * �ð� + �߷� * �ð�����
            pos.y = start.y + (v0 * percent) + (_gravity * percent * percent);
            transform.position = pos;

            yield return null;
        }

        _navAgent.CompleteOffMeshLink();
        _navAgent.isStopped = false;
    }

    private bool IsOnJump()
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
}
