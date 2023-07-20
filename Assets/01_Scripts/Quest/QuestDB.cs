using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestDataContainer")]
public class QuestDataContainer : ScriptableObject
{ 
    public List<QuestDB> questDataList = new List<QuestDB>();
}

[System.Serializable]
public class QuestDB
{
    public string questName;
    public string questDescription;
    public int requiredLv;
}
