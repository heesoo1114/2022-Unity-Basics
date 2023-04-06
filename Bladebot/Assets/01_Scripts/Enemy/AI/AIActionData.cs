using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIActionData : MonoBehaviour
{
    public bool TargetSpotted; // ���� �߰ߵǾ�����
    
    public Vector3 HitPoint;        // ���������� ���� ����
    public Vector3 HitNormal;       // ���������� ���� ������ �븻����
    public Vector3 LastSpotPoint;   // ���������� �߰ߵ� ����

    public bool IsArrived;          // �����ߴ���
    public bool IsAttacking;        // ������ �ϰ� �ִ���


}
