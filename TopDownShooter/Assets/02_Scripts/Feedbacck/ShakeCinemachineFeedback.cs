using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using static Define;

public class ShakeCinemachineFeedback : Feedback
{
    [SerializeField]
    private CinemachineVirtualCamera _cmVcam;
    [SerializeField]
    [Range(0, 5f)]
    private float _amplitude = 1, _frequency = 1;
    [SerializeField]
    [Range(0, 1f)]
    private float _duration = 0.1f;

    private CinemachineBasicMultiChannelPerlin _noise;

    private void OnEnable()
    {
        _cmVcam = VCam; // Define에서 가져오기
        _noise = _cmVcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>(); // Noise 제어
    }

    public override void CompletePrevFeedback()
    {
        StopAllCoroutines();
        _noise.m_AmplitudeGain = 0;
    }

    public override void CreateFeedback()
    {
        _noise.m_AmplitudeGain = _amplitude;
        _noise.m_FrequencyGain = _frequency;
        StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        float time = _duration;
        while(time > 0)
        {
            _noise.m_AmplitudeGain = Mathf.Lerp(0, _amplitude, time / _duration);
            
            yield return null;
            time -= Time.deltaTime;
        }
        _noise.m_AmplitudeGain = 0; // 진동을 꺼준다.
    }
}
