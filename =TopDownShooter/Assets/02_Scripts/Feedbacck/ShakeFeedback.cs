using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShakeFeedback : Feedback
{
    [SerializeField]
    private Transform _objectToShake;
    [SerializeField]
    private float _duration = 0.2f, _strength = 1f, _randomness = 90;
    // strength == 인텐시티
    [SerializeField]
    private int _vibrato = 10; // 프리퀀시
    [SerializeField]
    private bool _snapping = false, _fadeOut = false;

    public override void CompletePrevFeedback()
    {
        _objectToShake.DOComplete();
        // 모든 트윈을 종료시키고 완료된 트윈 갯수를 반환
    }

    public override void CreateFeedback()
    {
        CompletePrevFeedback();
        _objectToShake.DOShakePosition(_duration, _strength, _vibrato, _randomness, _snapping, _fadeOut);
    }
}
