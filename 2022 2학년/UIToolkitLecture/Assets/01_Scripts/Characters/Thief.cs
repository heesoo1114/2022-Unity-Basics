using UnityEngine;

public class Thief : MonoBehaviour
{
    private const string _name = "도적";
    private CharacterType _type = CharacterType.Melee;

    public void Introduce()
    {
        Debug.Log($"{_name} 이고 {_type} 입니다.");
    }
}
