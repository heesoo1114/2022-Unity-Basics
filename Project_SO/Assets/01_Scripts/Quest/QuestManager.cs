using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public QuestDataContainer questDataContainer;

    private void Start()
    {
        questDataContainer = Resources.Load<QuestDataContainer>("QuestDataContainer");

        foreach (QuestDB qdb in questDataContainer.questDataList)
        {
            Debug.Log($"����Ʈ �̸�: {qdb.questName}, ����Ʈ ����: {qdb.questDescription}, �ʼ� ���� {qdb.requiredLv}");
        }
    }

    // private void LoadQuestDB()
    // {
    //     questDataList = new List<QuestDB>();
    //     questDataList.Add(new QuestDB { questName = "ù ��° ����Ʈ", questDescription = "���͸� óġ�ϼ���", requiredLv = 5 });
    //     questDataList.Add(new QuestDB { questName = "�� ��° ����Ʈ", questDescription = "�������� �����ϼ���", requiredLv = 10 });
    // }
}
