using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DissolveFeedback : Feedback
{
    [SerializeField]
    private SkinnedMeshRenderer _meshRenderer;
    private MaterialPropertyBlock _matPropBlock;

    public UnityEvent OnAfterDissolve = null;

    private readonly int _isDissolveHash = Shader.PropertyToID("_IsDissolve");
    private readonly int _dissolveHeightHash = Shader.PropertyToID("_DissolveHeight");

    private void Awake()
    {
        _matPropBlock = new MaterialPropertyBlock();
        _meshRenderer.GetPropertyBlock(_matPropBlock);
    }

    public override void CreateFeedback()
    {
        StartCoroutine(MaterialDissolve());
    }

    private IEnumerator MaterialDissolve()
    {
        float dissolveTimeDuration = 2f;
        float current = 0;
        float dissolveHeightStart = 5f;
        float dissolveHeightEnd = -5f;
        float height = 0;

        _matPropBlock.SetInt(_isDissolveHash, 1);
        _meshRenderer.SetPropertyBlock(_matPropBlock);

        while (current < dissolveTimeDuration)
        {
            current += Time.deltaTime;
            height = Mathf.Lerp(dissolveHeightStart, dissolveHeightEnd, current / dissolveTimeDuration);

            _matPropBlock.SetFloat(_dissolveHeightHash, height);
            _meshRenderer.SetPropertyBlock(_matPropBlock);
            yield return null;
        }
        OnAfterDissolve?.Invoke();
    }

    public override void FinishFeedback()
    {
        StopAllCoroutines();
        _matPropBlock.SetInt(_isDissolveHash, 0);
        _matPropBlock.SetFloat(_dissolveHeightHash, 5f);

        _meshRenderer.SetPropertyBlock(_matPropBlock);
    }
}
