using UnityEngine;

public class UIScreen : MonoBehaviour
{
    [SerializeField]
    internal GameState screenState;

    [SerializeField]
    protected CanvasGroup canvasGroup;

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