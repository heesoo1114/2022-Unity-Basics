using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSplashFeedback : Feedback
{
    [SerializeField]
    private SplashEffectPlayer _splashPrefab;
    [SerializeField]
    private LayerMask _whatIsGround;
    [SerializeField]
    private Color _hitColor;
    private AIActionData _aiActionData;

    private void Awake()
    {
        _aiActionData = transform.parent.Find("AI").GetComponent<AIActionData>();   
    }

    public override void CreateFeedback()
    {
        SplashEffectPlayer sep = PoolManager.Instance.Pop(_splashPrefab.name) as SplashEffectPlayer;
        sep.transform.position = _aiActionData.HitPoint;

        RaycastHit hit;
        if (Physics.Raycast(sep.transform.position, Vector3.down, out hit, 10f, _whatIsGround))
        {
            sep.SetData(_hitColor, -hit.distance, _aiActionData.HitNormal);
            sep.StartPlay(3f);
        }
        else
        {
            Debug.Log("∂•¿Ã æ» ¥Í¿Ω");
        }
    }

    public override void FinishFeedback()
    {
        // do nothing
    }
}
