using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Monster", menuName = "Monster/add")]
public class MonsterSO : ScriptableObject
{
    public string monsterName;
    public int atk;
    public int hp;
    public int mp;
}
