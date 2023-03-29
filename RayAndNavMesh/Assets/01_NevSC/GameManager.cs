using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    

    public static GameManager Instance;

    [SerializeField]
    private LayerMask _whatIsBase;

    private Camera _mainCam;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("Multiple GameManager is running");
        }
        Instance = this;
    }
    private void Start()
    {
        _mainCam = Camera.main; //메인 카메라 캐싱
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftControl))
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool result = Physics.Raycast(ray, out hit, _mainCam.farClipPlane, _whatIsBase);
            if(result)
            {
                BaseBlock block = hit.collider.GetComponent<BaseBlock>();

                block?.ClickBaseBlock();
            }
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
