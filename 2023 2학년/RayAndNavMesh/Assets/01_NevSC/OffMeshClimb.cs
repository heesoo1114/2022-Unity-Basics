using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.AI;

public class OffMeshClimb : MonoBehaviour
{
    [SerializeField]
    private int _offMeshArea = 4; // 오프메시중 오르기 구역

    [SerializeField]
    private float _climbSpeed = 1.5f;
    private NavMeshAgent _navAgent;

    private void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
    }

    IEnumerator Start() // = start에서 startcoroutine과 같음
    {
        while (true)
        {
            yield return new WaitUntil(() => IsOnClimb());
            yield return StartCoroutine(ClimbOrDescend());
        }
    }

    private bool IsOnClimb()
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

    private IEnumerator ClimbOrDescend()
    {
        _navAgent.isStopped = true;
        OffMeshLinkData linkData = _navAgent.currentOffMeshLinkData;
        Vector3 start = linkData.startPos;
        Vector3 end = linkData.endPos;

        float climbTime = Mathf.Abs(end.y - start.y) / _climbSpeed;
        // 오르는 속도로 이 거리를 오른다고 가정할 때 몇 초가 걸리나

        float currentTime = 0;
        float percent = 0;
        
        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / climbTime;
            transform.position = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        _navAgent.CompleteOffMeshLink(); // 오픈메시링크를 탈출했음을 알림
        _navAgent.isStopped = false; // 움직임 재개
    }
}
