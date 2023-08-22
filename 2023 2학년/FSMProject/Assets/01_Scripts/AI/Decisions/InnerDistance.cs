using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDistance : AIDecision
{
    [Range(0.1f, 30f)]
    public float distance = 5f;
    public override bool MakeADecision()
    {
        float calc = Vector2.Distance(_brain.target.position, transform.position);

        if (calc < distance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (UnityEditor.Selection.activeObject == gameObject)
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, distance);
            Gizmos.color = oldColor;
        }
    }
#endif
}
