using UnityEngine;

public class UIScreen : MonoBehaviour
{
    [SerializeField] internal GameState state;

    [SerializeField] protected CanvasGroup canvasGroup;

    // open -> 스크린 활성, 비활성
    public virtual void UpdateScreenState(bool open)
    {
        canvasGroup.alpha = open ? 1 : 0;
        canvasGroup.interactable = open;
        canvasGroup.blocksRaycasts = open;
    }

    public virtual void Init()
    {

    }
}
