using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance;
    
    public void ResetTimeScle()
    {
        StopAllCoroutines();
        Time.timeScale = 1f;
    }

    public void ModifyTimeScale(float endTimeValue, float timeToWait, Action OnCompleteCallback)
    {
        StartCoroutine(TimeScaleCoroutine(endTimeValue, timeToWait, OnCompleteCallback));
    }

    IEnumerator TimeScaleCoroutine(float endTimeValue, float timeToWait, Action OnCompleteCallback)
    {
        yield return new WaitForSeconds(timeToWait);
        Time.timeScale = endTimeValue;
        OnCompleteCallback?.Invoke();
    }
}
