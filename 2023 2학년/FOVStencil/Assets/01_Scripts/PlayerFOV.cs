using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerFOV : MonoBehaviour
{
    [Range(0, 360)] public float viewAngle; //����
    public float viewRadius; //�ݰ�

    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _enemyFindDelay = 0.3f; //�� �����Ӹ����ϸ� ��������

    [SerializeField] private float _meshResolution;
    
    public List<Transform> visibleTargets = new List<Transform>();

    public Vector3 DirFromAngle(float degree, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            degree += transform.eulerAngles.y; //����ȸ��ġ��� �۷ι� ȸ��ġ�� �����Ѵ�.
        }

        float rad = degree * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
    }

    private void Start()
    {
        StartCoroutine(FindEnemyWithDelay(_enemyFindDelay));
    }

    private void Update()
    {
        DrawFieldOfView();
    }

    private void DrawFieldOfView()
    {
        int stempCount = Mathf.RoundToInt(viewAngle * _meshResolution);
        float stepAngleSize = viewAngle / stempCount;

        Vector3 pos = transform.position; 

        for (int i = 0; i <= stempCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle * 0.5f + stepAngleSize * i;
            Debug.DrawLine(pos, pos + DirFromAngle(angle, true) * viewRadius, Color.red);
        }
    }

    IEnumerator FindEnemyWithDelay(float delay)
    {
        var time = new WaitForSeconds(delay);
        while (true)
        {
            yield return time;
            FindVisibleEnemies();
        }
    }

    private void FindVisibleEnemies()
    {
        visibleTargets.Clear();

        Collider[] enemiesInView = new Collider[6];

        int cnt = Physics.OverlapSphereNonAlloc(transform.position, viewRadius, enemiesInView, _enemyMask);

        for (int i = 0; i < cnt; i++)
        {
            Transform enemy = enemiesInView[i].transform;
            Vector3 dir = enemy.position - transform.position; // ���� 
            Vector3 dirToEnemy = dir.normalized; // ���� ����

            if (Vector3.Angle(transform.forward, dirToEnemy) < viewAngle * 0.5f)
            {
                // �þ߹��� �ȿ� �ִٸ� ���̸� ���� �׾ȿ� �ƹ� ��ֹ��� ���ٴ� ���� �˾ƾ� ��
                if (!Physics.Raycast(transform.position, dirToEnemy, dir.magnitude, _obstacleMask))
                {
                    visibleTargets.Add(enemy);
                }
            }
        }
    }
}