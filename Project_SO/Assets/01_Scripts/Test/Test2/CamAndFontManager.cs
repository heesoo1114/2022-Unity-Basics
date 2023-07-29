using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CamAndFontManager : MonoBehaviour
{
    public CamAndFontSO camAndFontData;

    private void Start()
    {
        camAndFontData = Resources.Load<CamAndFontSO>("camAndFontData");

        Camera.main.backgroundColor = camAndFontData.backgroundColor;

        TextMeshProUGUI[] textmeshs = FindObjectsOfType<TextMeshProUGUI>();
        foreach (TextMeshProUGUI textmesh in textmeshs)
        {
            textmesh.color = camAndFontData.fontColor;
        }

        // GameObject backgroundImageObject = GameObject.FindGameObjectWithTag("BackgroundImage");
        // if (backgroundImageObject != null)
        // {
        //     SpriteRenderer backgroundImageRenderer = backgroundImageObject.GetComponent<SpriteRenderer>();
        // }
    }
}
