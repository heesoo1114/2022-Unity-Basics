using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DropDecal : PoolAbleMono
{
    private DecalProjector _decalProjector;

    [SerializeField]
    private float _defaultSize = 3f;

    private readonly int _alphaHash = Shader.PropertyToID("_Alpha");
    private readonly int _sizeHash = Shader.PropertyToID("_Size");

    public override void Init()
    {

    }

    private void Awake()
    {
        _decalProjector = GetComponent<DecalProjector>();
        // ��Į�� ���׸����� ��ȣ���� �����Ѵ�. (for ����ȭ)

        _decalProjector.material = new Material(_decalProjector.material); // �ش� ���׸����� ������ �ν��Ͻ��� �����.
    }

    public void StartSequence(Action EndCallback)
    {
        float value = 0;
        Material targetMat = _decalProjector.material;
        targetMat.SetFloat(_sizeHash, 0);

        DOTween.To(
            () => targetMat.GetFloat(_sizeHash),
            value => targetMat.SetFloat(_sizeHash, value),
            1,
            1f).SetEase(Ease.InOutSine).OnComplete(() => EndCallback?.Invoke());
    }

    public void FadeOut(float time)
    {
        Material targetMat = _decalProjector.material;
        DOTween.To(() => targetMat.GetFloat(_alphaHash), value => targetMat.SetFloat(_alphaHash, value), 0, time).OnComplete(GotoPool);

    }

    public void GotoPool()
    {
        PoolManager.Instance.Push(this);
    }

    public void SetUpSize(Vector3 size)
    {
        _decalProjector.size = size;
        _defaultSize = size.x;
    }
}
