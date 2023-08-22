using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PersonInfo
{
    public string Name;
    public string Info;
    public Sprite Image;
}

[CreateAssetMenu(menuName = "SO/PersonList")]
public class PersonLitSO : ScriptableObject
{
    public Sprite MenProfileImage;
    public Sprite WomenProfileImage;

    public List<PersonInfo> personList;
}
