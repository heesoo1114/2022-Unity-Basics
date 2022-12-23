using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartImage : MonoBehaviour
{
    private Image _image;
    [SerializeField] HeartImageSO _heartImageSo;
    // 이미지를 가지고 있어야 돼

    private void Awake()
    {
        _image = GetComponent<Image>();
    }

    public void SetHeartImage(bool value)
    {
        if(value == true)
        {
            _image.sprite = _heartImageSo.fullHeart;
        }
        else
        {
            _image.sprite = _heartImageSo.emptyHeart;
        }
    }
}
