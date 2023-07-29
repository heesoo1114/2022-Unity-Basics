using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WobbleTextEffect : MonoBehaviour
{
    private TMP_Text _tmpText;

    [SerializeField]
    private float _speed = 2f;

    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _tmpText.ForceMeshUpdate();

        TMP_TextInfo textInfo = _tmpText.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            if (charInfo.isVisible == false)
            {
                continue;
            }

            Vector3[] vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            int vIndex0 = charInfo.vertexIndex;
            Vector3 origin = vertices[vIndex0];

            for (int j = 1; j < 3; j++)
            {
                Vector3 current = vertices[vIndex0 + j];
                vertices[vIndex0 + j] = current + new Vector3(0, (Mathf.Sin(Time.time * _speed + origin.x) + 1) * 0.5f + 0.5f, 0);
            }
        }
        _tmpText.UpdateVertexData();
    }
}