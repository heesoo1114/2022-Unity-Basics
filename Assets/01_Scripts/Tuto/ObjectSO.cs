using UnityEngine;

[CreateAssetMenu]
public class ObjectSO : ScriptableObject
{
    public string monsterName;
    public float monsterAtk;
    public double monsterHP;
    public int monsterMoveSpeed;

    // SO는 텍스트로 직렬화 할 수 있는 오브젝트
}

// 클래스는 자료형

/*
    데이터 컨테이너
    ---------------
    데이터 필드 1 => 필드마다 특정 데이터 저장, 다양한 유형의 데이터 저장
    데이터 필드 2 
    데이터 필드 3
    ---------------
    데이터 조작 함수 => 데이터를 CRUD 및 접근 가능 (create, remove, update, delete = crud)
*/
