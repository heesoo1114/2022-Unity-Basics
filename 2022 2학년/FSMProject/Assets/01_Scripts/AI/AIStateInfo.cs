using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillName
{
    Melee = 0,
    Range = 1
}

public class AIStateInfo : MonoBehaviour
{
    public float MeleeCool = 0f;
    public float RangeCool = 0f;

    public bool IsMelee = false;
    public bool IsRange = false;

    public bool IsAttack = false;
}
