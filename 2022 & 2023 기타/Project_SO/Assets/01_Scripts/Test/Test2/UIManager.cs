using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance = null;

    TextMeshProUGUI textMeshProGUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        textMeshProGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // textMeshProGUI.text = "exp : " + GlobalData.Instance.playerExp;
    }
}
