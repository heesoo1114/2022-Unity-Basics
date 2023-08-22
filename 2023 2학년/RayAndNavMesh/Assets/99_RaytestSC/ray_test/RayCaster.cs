using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using System;
using UnityEngine;

public class RayCaster : MonoBehaviour
{
    [SerializeField]
    private float _maxDistance;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;

            // bool isHit = Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance);
            // 어디에서 쏠지, 어느 방향으로 쏠지, 충돌했다면 맞은 것을 어디에 저장할지, 어느 길이로 쏠지

            bool isHit = Physics.BoxCast(transform.position, transform.lossyScale * 0.5f, transform.forward,
                                         out hit, transform.rotation, _maxDistance);

            if (isHit)
            {
                Debug.Log("충돌");
                GameObject obj = hit.collider.gameObject;
                obj.GetComponent<Rigidbody>().AddForce(transform.forward * 100);
            }
        }
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;

        // bool isHit = Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance);
        // 어디에서 쏠지, 어느 방향으로 쏠지, 충돌했다면 맞은 것을 어디에 저장할지, 어느 길이로 쏠지

        bool isHit = Physics.BoxCast(transform.position, transform.lossyScale * 0.5f, transform.forward, 
                                     out hit, transform.rotation, _maxDistance);

        if (isHit)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);

            // 맞으면 그쪽에 박스 그려줌
            Gizmos.DrawCube(transform.position + transform.forward * hit.distance, transform.lossyScale);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * _maxDistance);
        }
    }
}
