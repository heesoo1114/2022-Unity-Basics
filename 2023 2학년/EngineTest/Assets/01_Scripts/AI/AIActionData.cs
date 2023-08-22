using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AIActionData : MonoBehaviour
{
    public bool TargetSpotted;      // 적이 발견되었는지

    public Vector3 HitPoint;        // 마지막으로 맞은 지점
    public Vector3 HitNormal;       // 마지막으로 맞은 지점의 노말벡터
    public Vector3 LastSpotPoint;   // 마지막으로 발견된 지점

    public bool IsArrived;          // 도착했는지
    public bool IsAttacking;        // 공격을 하고 있는지

    [field: SerializeField]
    public bool IsHit { get; set; }              // 현재 맞고 있는지

    public void Init()
    {
        TargetSpotted = false;
        IsArrived = false;
        IsAttacking = false;
        IsHit = false;
    }
}