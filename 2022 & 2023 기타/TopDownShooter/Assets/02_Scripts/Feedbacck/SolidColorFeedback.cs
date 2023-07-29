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
            // ���̴� �׷��� ���� bool Ÿ���� ���� ������ int 0, 1�� ture, false�� ����
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
