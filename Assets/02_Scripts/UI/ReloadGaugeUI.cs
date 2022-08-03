using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadGaugeUI : MonoBehaviour
{
    [SerializeField]
    private Transform _reloadBar;

    public void ReloadGaugeNormal(float value)
    {
        _reloadBar.localScale = new Vector3(value, 1, 1);
    }
}
