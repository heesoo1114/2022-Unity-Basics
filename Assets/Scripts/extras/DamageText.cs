using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    public TextMeshProUGUI DmgText => GetComponentInChildren<TextMeshProUGUI>();

    public void ReturnTextToPool() //end of Damage Text Animation
    {
        transform.SetParent(null);
        objectPooler.ReturnToPool(gameObject);
    }
}
