using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodScreen : MonoBehaviour
{
    [SerializeField]
    private float _screenTime = 0.3f;
    private Image _image;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.enabled = false;
    }

    public void ShowBloodScreen()
    {
        StopAllCoroutines();
        StartCoroutine(ShowCoroutine());
    }

    IEnumerator ShowCoroutine()
    {
        _image.enabled = true;
        yield return new WaitForSeconds(_screenTime);
        _image.enabled = false;
    }
}
