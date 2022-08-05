using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance;

    private void Awake()
    {
        Instance = this; // 나중 코드  변경
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void ResertTimeScale()
    {
        StopAllCoroutines();
        Time.timeScale = 1.0f;
    }

    public void ModifyTimeScale(float targetValue, float timeToWait, Action OnComplete = null)
    {
        StartCoroutine(TimeScaleCoroutine(targetValue, timeToWait, OnComplete));
    }

    // 0.05초 있다가 타임스켈 0.2로 떨어뜨려줘, () => 다시 코루틴을 실행시켜서 0.2초후에 1로 변경해줌
    IEnumerator TimeScaleCoroutine(float targetValue, float timeToWait, Action OnComplete = null)
    {
        yield return new WaitForSecondsRealtime(timeToWait); // 이녀석은 타임스케일에 영향을 받지 않는다.
        Time.timeScale = targetValue;
        OnComplete?.Invoke();
    }
}
