using UnityEngine;
using UnityEngine.U2D.Animation;
using DG.Tweening;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private SpriteLibraryAsset[] _spriteAssets;

    private SpriteLibrary _spriteLibrary;

    private int _currentSpriteIndex = 0;
    private readonly int _hashWaveDistance = Shader.PropertyToID("_WaveDistance");
    private Material _material;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteLibrary = GetComponent<SpriteLibrary>();
        _material = _spriteRenderer.GetComponent<Material>();
    }

    public void SetNextSprite()
    {
        _currentSpriteIndex = (_currentSpriteIndex + 1) % _spriteAssets.Length;
        _spriteLibrary.spriteLibraryAsset = _spriteAssets[_currentSpriteIndex];
    }

    private Tween _shockTween;
    private void ShockwaveEffect()
    {
        if (_shockTween != null && _shockTween.IsActive())
        {
            _shockTween.Kill();
        }

        _spriteRenderer.gameObject.SetActive(true);
        _material.SetFloat(_hashWaveDistance, -0.1f);

        _shockTween = DOTween.To(
            () => _material.GetFloat(_hashWaveDistance),
            value => _material.SetFloat(_hashWaveDistance, value), 1f, 0.6f)
            .OnComplete(() => _spriteRenderer.gameObject.SetActive(false)
            );
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetNextSprite();
        }
    }
}
