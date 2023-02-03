using UnityEngine;

public class Archer : MonoBehaviour
{
    private const string _name = "궁수 ";
    private CharacterType _type = CharacterType.Range;

    public void Introduce()
    {
        Debug.Log($"{_name} 이고 {_type} 입니다.");
    }
}
