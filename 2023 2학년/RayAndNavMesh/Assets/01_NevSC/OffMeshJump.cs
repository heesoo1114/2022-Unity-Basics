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

        float v0 = (end - start).y - _gravity; // y 방향의 최고점이고 이게 속도로 정의됨

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / jumpTime; // lerp용 시간

            Vector3 pos = Vector3.Lerp(start, end, percent);
            // 포물선 운동: 시작위치 + 초기속도 * 시간 + 중력 * 시간제곱
            pos.y = start.y + (v0 * percent) + (_gravity * percent * percent);
            transform.position = pos;

            yield return null;
        }

        _navAgent.CompleteOffMeshLink();
        _navAgent.isStopped = false;
    }

    private bool IsOnJump()
    {
        if (_navAgent.isOnOffMeshLink) // 오픈 메시링크 상에 에이전트가 올라와 있나
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
