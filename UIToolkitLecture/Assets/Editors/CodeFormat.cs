#if UNITY_EDITOR
public class CodeFormat 
{
    public static string CharacterFormat = 
@"using UnityEngine;

public class {0} : MonoBehaviour
{{
    private const string _name = ""{1}"";
    private CharacterType _type = CharacterType.{2};

    public void Introduce()
    {{
        Debug.Log($""{{_name}} 이고 {{_type}} 입니다."");
    }}
}}
";
}
#endif

// 바꿀 부분에 숫자 넣고
// 그 숫자 순서대로 문자 들어감