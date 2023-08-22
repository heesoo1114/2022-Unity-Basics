using UnityEngine;

public class Warrior : MonoBehaviour
{
    private const string _name = "전사";
    private CharacterType _type = CharacterType.Melee;

    public void Introduce()
    {
        Debug.Log($"{_name} 이고 {_type} 입니다.");
    }
}
