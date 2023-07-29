using UnityEngine;
using System.IO;
using UnityEditor;

public class CsvReader : MonoBehaviour
{
    private static string csvFilePath = "/Resources/monsterCSV.csv";

    [MenuItem("Util/VSCtoSO")]
    public static void setMonster()
    {
        string[] _strData = File.ReadAllLines(Application.dataPath + csvFilePath);
        foreach (string sData in _strData)
        {
            string[] _data = sData.Split(',');

            MonsterSO monster = ScriptableObject.CreateInstance<MonsterSO>();
            monster.monsterName = _data[0];
            monster.atk = int.Parse(_data[1]);
            monster.hp = int.Parse(_data[2]);
            monster.mp = int.Parse(_data[3]);

            AssetDatabase.CreateAsset(monster, $"Assets/Monster/{monster.monsterName}.asset");
        }
    }
}
