using UnityEngine;

public class UIScreen : MonoBehaviour
{
    [SerializeField] internal GameState state;

    [SerializeField] protected CanvasGroup canvasGroup;

    // open -> ��ũ�� Ȱ��, ��Ȱ��
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
