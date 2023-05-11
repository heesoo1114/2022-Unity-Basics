using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField]
    private PoolingListSO _poolingList;

    private Transform _playerTrm;
    public Transform PlayerTrm
    {
        get
        {
            if (_playerTrm == null)
            {
                _playerTrm = GameObject.FindGameObjectWithTag("Player").transform;
            }
            return _playerTrm;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager is running");
        }
        Instance = this;

        CreatePool();
        CreateTimeContoller();
        CreateUIManager();
    }

    private void CreatePool()
    {
        PoolManager.Instance = new PoolManager(transform);
        _poolingList.Pairs.ForEach(pair =>
        {
            PoolManager.Instance.CreatePool(pair.Prefab, pair.Count);
        });
    }
    private void CreateTimeContoller()
    {
        TimeController.Instance = gameObject.AddComponent<TimeController>();
    }

    private void CreateUIManager()
    {
        UIDocument uiDocument = GameObject.FindObjectOfType<UIDocument>();
        UIManager.Instance = uiDocument.gameObject.AddComponent<UIManager>();
    }

    #region 디버그 모드
    [SerializeField]
    private LayerMask _whatIsGround;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Ray ray = Define.MainCam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            // 수행
            bool result = Physics.Raycast(ray, out hit, Define.MainCam.farClipPlane, _whatIsGround);

            if (result)
            {
                EnemyController e = PoolManager.Instance.Pop("HammerEnemy") as EnemyController;
                e.transform.SetPositionAndRotation(hit.point, Quaternion.identity);
            }
        }
    }

    #endregion
}
