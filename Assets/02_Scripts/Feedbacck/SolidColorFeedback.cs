using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidColorFeedback : Feedback
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private float _flashTime = 0.1f;

    public override void CompletePrevFeedback()
    {
        StopAllCoroutines();
        _spriteRenderer.material.SetInt("_MakeHit", 0);
    }

    public override void CreateFeedback()
    {
        if(_spriteRenderer.material.HasProperty("_MakeHit"))
        {
            // 셰이더 그래프 언어는 bool 타입이 없기 때문에 int 0, 1로 ture, false를 구분
            _spriteRenderer.material.SetInt("_MakeHit", 1);
            StartCoroutine(WaitBeforeChngeingBack());
        }
    }

    IEnumerator WaitBeforeChngeingBack()
    {
        yield return new WaitForSeconds(_flashTime);
        _spriteRenderer.material.SetInt("_MakeHit", 0);
    }
}
