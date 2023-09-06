using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public struct ViewCastInfo
{
    public bool hit;
    public Vector3 point;
    public float distance;
    public float angle;
}

public struct EdgeInfo
{
    public Vector3 pointA;
    public Vector3 pointB;
}

public class PlayerFOV : MonoBehaviour
{
    [Range(0, 360)] public float viewAngle; //각도
    public float viewRadius; //반경

    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private float _enemyFindDelay = 0.3f; //매 프레임마다하면 성능저하

    [SerializeField] private float _meshResolution;
    [SerializeField] private int _edgeResolveInteractions; // 3회
    [SerializeField] private float _edgeDistanceThreshold;

    private MeshFilter _viewMeshFilter;
    private Mesh _viewMesh;

    public List<Transform> visibleTargets = new List<Transform>();

    private void Awake()
    {
        _viewMeshFilter = transform.Find("ViewVisual").GetComponent<MeshFilter>();
        _viewMesh = new Mesh();
        _viewMesh.name = "Sight_Mesh";
        _viewMeshFilter.mesh = _viewMesh;
    }

    public Vector3 DirFromAngle(float degree, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            degree += transform.eulerAngles.y; //로컬회전치라면 글로벌 회전치로 변경한다.
        }

        float rad = degree * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));
    }

    private void Start()
    {
        StartCoroutine(FindEnemyWithDelay(_enemyFindDelay));
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
            Vector3 dir = enemy.position - transform.position;
            Vector3 dirToEnemy = dir.normalized;

            if (Vector3.Angle(transform.forward, dirToEnemy) < viewAngle * 0.5f)
            {
                //시야범위 안에 있다면 레이를 쏴서 그안에 아무 장애물도 없다는 것을 알아내야 해
                if (!Physics.Raycast(transform.position, dirToEnemy, dir.magnitude, _obstacleMask))
                {
                    visibleTargets.Add(enemy);
                }
            }
        }

    }

    private void LateUpdate()
    {
        DrawFieldOfView();
    }

    private EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast)
    {
        float minAngle = minViewCast.angle;
        float maxAngle = maxViewCast.angle;

        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < _edgeResolveInteractions; i++)
        {
            float angle = (minAngle + maxAngle) * 0.5f; // 절반으로 이분
            var castInfo = ViewCast(angle); // 새롭게 캐스트를 쏜다.

            // 거리가 지정된 쓰레스 홀드보다 커졌는가?
            bool distanceExceed = Mathf.Abs(minViewCast.distance - castInfo.distance) > _edgeDistanceThreshold;

            // 피격상태가 이전상태와 동일하고 거리도 일정거리 이내라면
            if (castInfo.hit == minViewCast.hit && !distanceExceed)
            {
                minAngle = angle;
                minPoint = castInfo.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = castInfo.point;
            }
        }

        return new EdgeInfo { pointA = minPoint, pointB = maxPoint };
    }

    private void DrawFieldOfView()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * _meshResolution);
        float stepAngleSize = viewAngle / stepCount;

        Vector3 pos = transform.position;

        List<Vector3> viewPoints = new List<Vector3>();
        var oldViewCastInfo = new ViewCastInfo();
        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle * 0.5f + stepAngleSize * i;
            //Debug.DrawLine(pos, pos + DirFromAngle(angle, true) * viewRadius, Color.red);

            var info = ViewCast(angle);

            if (i > 0)
            {
                // 이미 한번 쓴 기록이 old에 남아있을 거니까 여기서부터 비교를 하면 돼
                bool disstanceExceeded = Mathf.Abs(oldViewCastInfo.distance - info.distance) > _edgeDistanceThreshold;

                if (oldViewCastInfo.hit != info.hit || (oldViewCastInfo.hit && info.hit && disstanceExceeded))
                {
                    var edge = FindEdge(oldViewCastInfo, info);


                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }

                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }

            viewPoints.Add(info.point);

            oldViewCastInfo = info; // 이전 인포에 현재 인포를 저장
        }

        int vertCount = viewPoints.Count + 1; //왜?
        Vector3[] vertices = new Vector3[vertCount];
        int[] triangles = new int[(vertCount - 2) * 3];

        vertices[0] = Vector3.zero;

        for (int i = 0; i < vertCount - 1; i++)
        {
            //정점을 넣어주고
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertCount - 2)
            {
                int tIndex = i * 3;
                triangles[tIndex + 0] = 0;
                triangles[tIndex + 1] = i + 1;
                triangles[tIndex + 2] = i + 2;
            }
        }
        _viewMesh.Clear();
        _viewMesh.vertices = vertices;
        _viewMesh.triangles = triangles;
        _viewMesh.RecalculateNormals(); // = (_viewMeshFilter.mesh = _viewMesh);
    }

    private ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = DirFromAngle(globalAngle, true);

        if (Physics.Raycast(transform.position, dir, out RaycastHit hit, viewRadius, _obstacleMask))
        {
            return new ViewCastInfo
            {
                hit = true,
                point = hit.point,
                distance = hit.distance,
                angle = globalAngle
            };
        }
        else
        {
            return new ViewCastInfo
            {
                hit = false,
                point = transform.position + dir * viewRadius,
                distance = viewRadius,
                angle = globalAngle
            };
        }
    }
}
