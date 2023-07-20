using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    private string playerName;

    [SerializeField]
    private int level;

    [SerializeField]
    private int hp;

    [SerializeField]
    private int dmg;

    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    public int Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    public int Dmg
    {
        get { return dmg; }
        set { dmg = value; }
    }
}
