using UnityEngine;

public class CharacterPluggableBehavior : MonoBehaviour
{
    private BaseBehavior nowBehavior;

    private void Update()
    {
        if (nowBehavior != null)
        {
            nowBehavior.Execute(); // 현재 상태 실행
        }
    }

    public void ChangeBehavior(BaseBehavior newBehavior, bool prioritize = false)
    {
        if (prioritize || nowBehavior != newBehavior)
        {
            nowBehavior = newBehavior;
        }
    }
}
