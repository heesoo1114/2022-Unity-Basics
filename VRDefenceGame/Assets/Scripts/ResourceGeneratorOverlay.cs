using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;
    private ResourceGeneratorData resourceGeneratorData;
    private Transform barTransform;

    private void Start()
    {
        resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetAmountGeneratedPerSecond().ToString("F1"));
        barTransform = transform.Find("bar");
    }

    private void Update()
    {
        barTransform.localScale = new Vector3(1-resourceGenerator.GetTimerNormalized(), 1, 1);
    }
}
