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
        Debug.Log($"직렬화 된 데이터 {json}");

        SerializeDB retDB = JsonUtility.FromJson<SerializeDB>(json);
        Debug.Log($"복원된 데이터 - intValue : {retDB.intValue}");
        Debug.Log($"복원된 데이터 - flotValue : {retDB.floatValue}");
        Debug.Log($"복원된 데이터 - stringValue : {retDB.stringValue}");
    }
}
