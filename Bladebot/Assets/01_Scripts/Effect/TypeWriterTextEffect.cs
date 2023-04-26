using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using System;
using Cinemachine.Utility;

public class TypeWriterTextEffect : MonoBehaviour
{
    [SerializeField]
    private float _typeTime = 0.1f;

    [SerializeField]
    private Color _startColor, _endColor;

    private TMP_Text _tmpText;
    private int _tIndex = 0;
    private bool _isTyping = false;

    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && _isTyping == false)
        {
            _isTyping = true;
            StartEffect("Hi! this is GGM! 안녕하세요");
        }
    }

    private void StartEffect(string text)
    {
        _tmpText.SetText(text);
        _tmpText.maxVisibleCharacters = 0;
        _tIndex = 0;
        _tmpText.ForceMeshUpdate();

        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        TMP_TextInfo textInfo = _tmpText.textInfo;
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            yield return StartCoroutine(TypeOneChar(textInfo));
        }
        _isTyping = false;
    }

    private IEnumerator TypeOneChar(TMP_TextInfo textInfo)
    {
        _tmpText.maxVisibleCharacters = _tIndex + 1;
        _tmpText.ForceMeshUpdate();

        TMP_CharacterInfo charInfo = textInfo.characterInfo[_tIndex];

        if (charInfo.isVisible == false)
        {
            yield return new WaitForSeconds(_typeTime); // 한 글자 타이핑 시간만큼 기다려주기만 하고 끝
        }
        else
        {
            Vector3[] vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;
            Color32[] colors = textInfo.meshInfo[charInfo.materialReferenceIndex].colors32;

            int vIndex0 = charInfo.vertexIndex;
            int vIndex1 = vIndex0 + 1;
            int vIndex2 = vIndex0 + 2;
            int vIndex3 = vIndex0 + 3; // 나중

            Vector3 v1Origin = vertices[vIndex1];
            Vector3 v2Origin = vertices[vIndex2];

            float currentTime = 0;
            float percent = 0;

            while (percent < 1)
            {
                currentTime += Time.deltaTime;
                percent = currentTime / _typeTime;

                float yDelta = Mathf.Lerp(2f, 0, percent);

                vertices[vIndex1] = v1Origin + new Vector3(0, yDelta, 0);
                vertices[vIndex2] = v2Origin + new Vector3(0, yDelta, 0);

                // 나중에 컬러 수정

                _tmpText.UpdateVertexData();
                yield return null;
            }
        }

        _tIndex++;
    }
}
