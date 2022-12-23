using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Experimental.Rendering.Universal;

public class FlashLightFeedback : Feedback
{
    [SerializeField]
    private Light2D _lightTarget = null;
    [SerializeField]
    private float _lightOnDelay = 0.01f, _lightOffDelay = 0.01f;
    [SerializeField]
    private bool _defaultState = false;


    public override void CompletePrevFeedback()
    {
        StopAllCoroutines(); // 현재 MonoBehaviour 에 돌고 있는 코루틴만 종료 
        _lightTarget.enabled = _defaultState;
    }

    IEnumerator ToggleLightCo(float time, bool result, Action Callback = null)
    {
        yield return new WaitForSeconds(time);
        _lightTarget.enabled = result;
        Callback?.Invoke();
    }

    public override void CreateFeedback()
    {
        StartCoroutine(ToggleLightCo(_lightOnDelay, true, () =>
        {
            StartCoroutine(ToggleLightCo(_lightOffDelay, false));
        }));
    }
}
