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
            Debug.Log($"퀘스트 이름: {qdb.questName}, 퀘스트 설명: {qdb.questDescription}, 필수 레벨 {qdb.requiredLv}");
        }
    }

    // private void LoadQuestDB()
    // {
    //     questDataList = new List<QuestDB>();
    //     questDataList.Add(new QuestDB { questName = "첫 번째 퀘스트", questDescription = "몬스터를 처치하세요", requiredLv = 5 });
    //     questDataList.Add(new QuestDB { questName = "두 번째 퀘스트", questDescription = "아이템을 차지하세요", requiredLv = 10 });
    // }
}
