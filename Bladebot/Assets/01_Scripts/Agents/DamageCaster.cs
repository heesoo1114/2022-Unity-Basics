using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DamageCaster : MonoBehaviour
{
    [SerializeField]
    [Range(0.5f, 3f)]
    private float _casterRadius = 1f;

    [SerializeField]
    private float _casterInterpolation = 0.5f;

    [SerializeField]
    private LayerMask _targetLayer;

    [SerializeField]
    private int _damage = 10;

    public void CastDamage()
    {
        Vector3 startPos = transform.position - transform.forward * _casterRadius;

        RaycastHit hit;
        bool isHit = Physics.SphereCast(startPos, _casterRadius, transform.forward, out hit, _casterRadius + _casterInterpolation, _targetLayer);

        if (isHit)
        {
            // Debug.Log($"맞았습니다. {hit.collider.name}");
            if (hit.collider.TryGetComponent<IDamageAble>(out IDamageAble health))
            {
                health.OnDamage(_damage, hit.point, hit.normal);
            }
        }
        else
        {
            Debug.Log("안 맞았습니다.");
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _casterRadius);
            Gizmos.color = Color.white;
        }
    }
#endif
}
