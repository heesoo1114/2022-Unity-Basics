using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UIElements;

public class Ex2 : MonoBehaviour
{
    private void OnEnable()
    {
        UIDocument ui = GetComponent<UIDocument>();
        VisualElement element = ui.rootVisualElement;

        Button ToggleButton = element.Q<Button>("BtnShow");

        ToggleButton.RegisterCallback<ClickEvent>(evt =>
        {
            List<VisualElement> cardList = element.Query<VisualElement>(className: "card").ToList(); 
            StartCoroutine(RowCard(cardList));
        });
    }

    IEnumerator RowCard(List<VisualElement> cardList)
    {
        foreach (VisualElement card in cardList)
        {
            ShowCard(card);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void ShowCard(VisualElement card)
    {
        if (card != null && card.ClassListContains("on"))
        {
            card.RemoveFromClassList("on");
        }
        else if (card != null)
        {
            card.AddToClassList("on");
        }
    }
}