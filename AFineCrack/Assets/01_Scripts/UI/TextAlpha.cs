using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextAlpha : MonoBehaviour 
{
    [SerializeField] private float _fadeTime;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        UIDisolove();
    }

    private void UIDisolove()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_text.DOColor(new Color(1, 1, 1, 0), _fadeTime).SetLoops(2, LoopType.Yoyo));
        sequence.AppendCallback(() => UIDisolove());
    }

}