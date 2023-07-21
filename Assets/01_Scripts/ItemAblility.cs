using System;
using UnityEngine;

public enum CharacterStack
{
    Int = 0, 
    Hp = 1,
    Str = 2
}

[Serializable]
public class ItemAblility
{
    // ������ �ɷ�ġ
    public CharacterStack characterStack;
    public int valStack;

    [SerializeField]
    private int min;

    [SerializeField]
    private int max;

    // �б� ����
    public int Min => min;
    public int Max => max;

    public ItemAblility(int min, int max)
    {
        this.min = min; 
        this.max = max;

        getStackVal();
    }

    public void getStackVal()
    {
        valStack = UnityEngine.Random.Range(min, max);
    }

    public void addStackVal(ref int v)
    {
        v += valStack;
    }
}
