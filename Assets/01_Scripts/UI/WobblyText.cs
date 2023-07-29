using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WobblyText : MonoBehaviour
{
    // private TMP_Text tmpText;
    private TextMeshProUGUI tmpText;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        tmpText.ForceMeshUpdate();
        TMP_TextInfo textInfo = tmpText.textInfo;

        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];

            if (charInfo.isVisible == false) continue;

            Vector3[] vertices = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            int vIndex0 = charInfo.vertexIndex;

            for (int j = 0; j < 4; j++)
            {
                Vector3 origin = vertices[vIndex0 + j];
                vertices[vIndex0 + j] = origin + new Vector3(0, Mathf.Sin(Time.time * 5f + origin.x), 0);
            }
        }

        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;

            tmpText.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}