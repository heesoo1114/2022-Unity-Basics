using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializeDataBase : MonoBehaviour
{
    public SerializeDB db;

    private void Start()
    {
        db = new SerializeDB
        {
            intValue = 10,
            floatValue = 3.14f,
            stringValue = "Hello, SO!"
        };

        string json = JsonUtility.ToJson(db);
        Debug.Log($"����ȭ �� ������ {json}");

        SerializeDB retDB = JsonUtility.FromJson<SerializeDB>(json);
        Debug.Log($"������ ������ - intValue : {retDB.intValue}");
        Debug.Log($"������ ������ - flotValue : {retDB.floatValue}");
        Debug.Log($"������ ������ - stringValue : {retDB.stringValue}");
    }
}
