using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Node : MonoBehaviour
{
    public static Action<Node> OnNodeSelected;

    public Turret Turret { get; set; }

    public void SetTurret(Turret turret)
    {
        Turret = turret;
    }

    public bool IsEmpty()
    {
        return Turret == null;
    }

    public void SetlectTurret()
    {
        OnNodeSelected?.Invoke(this);
    }
}
