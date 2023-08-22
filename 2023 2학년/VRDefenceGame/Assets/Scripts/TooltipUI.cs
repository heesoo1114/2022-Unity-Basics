using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance { get; private set; }

    [SerializeField] private RectTransform canRectTransform;
    private TextMeshProUGUI textMeshPro;
    private RectTransform backgrountRectTransform;
    private RectTransform rectTransform;

    private TooltipTimer tooltipTimer;

    public class TooltipTimer
    {
        public float timer;
    }

    private void Awake()
    {
        Instance = this;
        textMeshPro = transform.Find("text").GetComponent<TextMeshProUGUI>();
        backgrountRectTransform = transform.Find("background").GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
        Hide();
    }

   
    private void Update()
    {
        HandleFollowMouse();
        if (tooltipTimer != null)
        {
            tooltipTimer.timer -= Time.deltaTime;
            if (tooltipTimer.timer <= 0)
            {
                Hide();
            }
        }
    }

    private void HandleFollowMouse()
    {
        Vector2 anchoredPosition = Input.mousePosition / canRectTransform.localScale.x;

        if (anchoredPosition.x + backgrountRectTransform.rect.width > canRectTransform.rect.width)
        {
            anchoredPosition.x = canRectTransform.rect.width - backgrountRectTransform.rect.width;
        }
        if (anchoredPosition.y + backgrountRectTransform.rect.height > canRectTransform.rect.height)
        {
            anchoredPosition.y = canRectTransform.rect.height - backgrountRectTransform.rect.height;
        }
        if (anchoredPosition.x < 0)
        {
            anchoredPosition.x = 0;
        }
        if (anchoredPosition.y < 0)
        {
            anchoredPosition.y = 0;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void SetText(string tooltipText)
    {
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);
        backgrountRectTransform.sizeDelta = textSize+padding;
    }

    public void Show(string tooltipText, TooltipTimer timer=null)
    {
        this.tooltipTimer = timer;
        gameObject.SetActive(true);
        SetText(tooltipText);
        HandleFollowMouse();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
