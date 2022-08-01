using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchAnimation : MonoBehaviour
{
    [SerializeField]
    private bool _changeRadius; // Èçµé¸±‹š ¹Ý°æ

    [SerializeField]
    private float _intensityRandom, radiusRandom, _timeRandom;

    private float _baseIntensity;
    private float _baseTime = 1f;
    private float _baseRadius;

    private Sequence _seq = null;
    private Light2D _light;

    private void Awake()
    {
        _light = transform.Find("Light").GetComponent<Light2D>();
        _baseIntensity = _light.intensity;
        _baseRadius = _light.pointLightOuterRadius;
    }

    private void OnEnable()
    {
        ShakeLight();
    }

    private void ShakeLight()
    {
        float targetInten = _baseIntensity + Random.Range(-_intensityRandom, +_intensityRandom);
        float targetTime = _baseTime + Random.Range(-_timeRandom, +_timeRandom);

        if (!gameObject.activeSelf) return;
        _seq = DOTween.Sequence();
        _seq.Append(DOTween.To(
            () => _light.intensity,
            value => _light.intensity = value,
            targetInten,
            targetTime
        ));

        _seq.AppendCallback(() => ShakeLight());

    }

}
