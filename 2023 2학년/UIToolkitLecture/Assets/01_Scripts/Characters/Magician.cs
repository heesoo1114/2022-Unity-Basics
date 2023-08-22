using UnityEngine;

public class Magician : MonoBehaviour
{
    private const string _name = "마법사";
    private CharacterType _type = CharacterType.Range;

    public void Introduce()
    {
        Debug.Log($"{_name} 이고 {_type} 입니다.");
    }
}
