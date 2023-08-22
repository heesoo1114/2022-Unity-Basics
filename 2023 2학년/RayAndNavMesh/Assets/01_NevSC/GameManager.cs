using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private LayerMask _whatIsBase;

    private Camera _mainCam;

    private NavMeshSurface _navSurface;

    private NavMeshAgent _navAgent;

    private Vector3 _target;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager is running");
        }
        Instance = this;

        _navSurface = GetComponent<NavMeshSurface>();
        _navAgent = FindObjectOfType<NavMeshAgent>();
    }

    private void ReBakeMesh()
    {
        _navSurface.BuildNavMesh();
    }

    private void Start()
    {
        _mainCam = Camera.main; //메인 카메라 캐싱
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool result = Physics.Raycast(ray, out hit, _mainCam.farClipPlane, _whatIsBase);
            if (result)
            {
                BaseBlock block = hit.collider.GetComponent<BaseBlock>();

                block?.ClickBaseBlock();
            }
            ReBakeMesh();
            _navAgent.SetDestination(hit.point);
        }
    }

    public bool GetMouseWorldPosition(out Vector3 pos)
    {
        Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool result = Physics.Raycast(ray, out hit, _mainCam.farClipPlane, _whatIsBase);
        if (result)
        {
            pos = hit.point;
            return true;
        }
        pos = Vector3.zero;
        return false;
    }
}