using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Feedback : MonoBehaviour
{
    public abstract void CreateFeedback(); // ���� �ǵ�� ����
    public abstract void CompletePrevFeedback(); // ���� �ǵ�� ����

    protected virtual void OnDestroy()
    {
        CompletePrevFeedback();
    }

    protected virtual void OnDisable()
    {
        CompletePrevFeedback();
    }
}
