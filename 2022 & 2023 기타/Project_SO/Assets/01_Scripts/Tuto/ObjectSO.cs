using UnityEngine;

[CreateAssetMenu]
public class ObjectSO : ScriptableObject
{
    public string monsterName;
    public float monsterAtk;
    public double monsterHP;
    public int monsterMoveSpeed;

    // SO�� �ؽ�Ʈ�� ����ȭ �� �� �ִ� ������Ʈ
}

// Ŭ������ �ڷ���

/*
    ������ �����̳�
    ---------------
    ������ �ʵ� 1 => �ʵ帶�� Ư�� ������ ����, �پ��� ������ ������ ����
    ������ �ʵ� 2 
    ������ �ʵ� 3
    ---------------
    ������ ���� �Լ� => �����͸� CRUD �� ���� ���� (create, remove, update, delete = crud)
*/
