using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput2 : MonoBehaviour
{
    [SerializeField] private Button _lBtn;
    [SerializeField] private Button _rBtn;

    private void Update()
    {
        if (_lBtn.interactable && _lBtn.enabled)
        {
            Debug.Log("Press LBtn");
        }

        if (_lBtn.interactable && _lBtn.enabled)
        {
            Debug.Log("Press RBtn");
        }
    }
}
