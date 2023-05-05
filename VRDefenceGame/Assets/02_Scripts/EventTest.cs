using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTest : MonoBehaviour
{
    public void OnFirstHoverEntered()
    {
        Debug.Log($"{gameObject} - OnFirstHoverEntered");
    }

    public void OnLastHoverExited()
    {
        Debug.Log($"{gameObject} - OnLastHoverExited");
    }

    public void OnHoverEntered()
    {
        Debug.Log($"{gameObject} - OnHoverEntered");
    }

    public void OnHoverExited()
    {
        Debug.Log($"{gameObject} - OnHoverExited");
    }

    public void OnFirstSelectEntered()
    {
        Debug.Log($"{gameObject} - OnFirstSelectEntered");
    }

    public void OnLastSelectExited()
    {
        Debug.Log($"{gameObject} - OnLastSelectExited");
    }

    public void OnSelectEntered()
    {
        Debug.Log($"{gameObject} - OnSelectEntered");
    }

    public void OnSelectExited()
    {
        Debug.Log($"{gameObject} - OnSelectExited");
    }

    public void OnActivated()
    {
        Debug.Log($"{gameObject} - OnActivated");
    }

    public void OnDeactivated()
    {
        Debug.Log($"{gameObject} - OnDeactivated");
    }
}
