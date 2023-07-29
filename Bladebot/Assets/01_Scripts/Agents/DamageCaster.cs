using UnityEngine;

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

    private AgentController _controller;

    private void Awake()
    {
        _controller = transform.parent.GetComponent<AgentController>();
    }

    public void CastDamage()
    {
        Vector3 startPos = transform.position - transform.forward * _casterRadius;

        RaycastHit[] hits;
        hits = Physics.SphereCastAll(startPos, _casterRadius, transform.forward, _casterRadius + _casterInterpolation, _targetLayer);

        foreach (RaycastHit h in hits)
        {
            if (h.collider.TryGetComponent<IDamageAble>(out IDamageAble health))
            {
                if (h.point.sqrMagnitude == 0) continue;

                float dice = Random.value; // 0 ~ 1 까지의 값
                int damage = _controller.CharData.BaseDamage;
                int fontSize = 10;
                Color fontColor = Color.white;

                // 크리티컬 계산
                if (dice < _controller.CharData.BaseCritical)
                {
                    damage = Mathf.CeilToInt(damage * _controller.CharData.BaseCriticalDamage);
                    fontSize = 15;
                    fontColor = Color.red;
                }
                health.OnDamage(damage, h.point, h.normal);

                PopupText text = PoolManager.Instance.Pop("PopupText") as PopupText;
                text.StartPopup(text: damage.ToString(), pos: h.point + new Vector3(0, 0.5f), fontSize: fontSize, color: fontColor);
            }
            else
            {
                Debug.Log("안 맞았습니다");
            }
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
