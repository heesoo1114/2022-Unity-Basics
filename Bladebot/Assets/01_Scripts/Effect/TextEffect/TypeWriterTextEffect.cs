using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using System;
using Cinemachine.Utility;
using UnityEditor;
using System.Security.Cryptography;
using UnityEngine.UIElements;

public class TypeWriterTextEffect : MonoBehaviour
{
    [SerializeField]
    private float _typeTime = 0.1f;

    [SerializeField]
    private Color _startColor, _endColor;

    [SerializeField]
    private ParticleSystem _particlePrefab;

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
        else if (Input.GetKeyDown(KeyCode.A) && _isTyping == true)
        {
            StopEffect();
        }
    }

    private void StartEffect(string text)
    {
        _tmpText.SetText(text);
        _tmpText.maxVisibleCharacters = 0;
        _tIndex = 0;
        _tmpText.color = _endColor;
        _tmpText.ForceMeshUpdate();

        StartCoroutine(TypeText());
    }

    private void StopEffect()
    {
        StopAllCoroutines();
        TMP_TextInfo textInfo = _tmpText.textInfo;
        _tmpText.maxVisibleCharacters = textInfo.characterCount;
        _tmpText.ForceMeshUpdate();
        for (int i = _tIndex; i < textInfo.characterCount; i++)
        {
            StartCoroutine(TypeOneChar(textInfo, i));
        }
        _isTyping = false;
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

    private IEnumerator TypeOneChar(TMP_TextInfo textInfo, int idx = -1)
    {
        if (idx < 0)
        {
            _tmpText.maxVisibleCharacters = _tIndex + 1;
            _tmpText.ForceMeshUpdate();
        }

        TMP_CharacterInfo charInfo = textInfo.characterInfo[idx < 0 ? _tIndex : idx];

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
            int vIndex3 = vIndex0 + 3; // 나중 (파티클에서 사용)

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

                // 컬러 
                for (int i = 0; i < 4; i++)
                {
                    colors[vIndex0 + i] = Color.Lerp(_startColor, _endColor, percent);
                }

                _tmpText.UpdateVertexData(TMP_VertexDataUpdateFlags.Vertices | TMP_VertexDataUpdateFlags.Colors32);

                yield return null;
            }

            // TransformPoint: local좌표를 world좌표로 변환해서 넣어줌
            Vector3 worldParticlePos = transform.TransformPoint(vertices[vIndex3]);

            ParticleSystem typeEffect = Instantiate(_particlePrefab);
            typeEffect.transform.position = worldParticlePos;
            typeEffect.transform.rotation = Quaternion.Euler(-90, 0, 0);
            typeEffect.transform.parent = transform;
            typeEffect.Play();
            
        }

        _tIndex++;
    }
}
