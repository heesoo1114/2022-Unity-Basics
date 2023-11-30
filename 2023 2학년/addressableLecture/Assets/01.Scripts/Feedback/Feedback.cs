using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    public abstract void CreateFeedback();
    public abstract void CompletePrevFeedback();

    protected virtual void OnDestroy()
    {
        CompletePrevFeedback();
    }

    protected virtual void OnDisable()
    {
        CompletePrevFeedback();
    }
}