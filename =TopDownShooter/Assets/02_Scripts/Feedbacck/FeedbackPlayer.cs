using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    [SerializeField]
    private List<Feedback> _feedbackToPlay = null;
    
    public void PlayFeedback()
    {
        FinishFeedback(); // �������� ���� �ǵ���� �������ְ� �����Ѵ�.
        foreach (Feedback f in _feedbackToPlay)
        {
            f.CreateFeedback();
        }
    }

    public void FinishFeedback()
    {
        foreach(Feedback f in _feedbackToPlay)
        {
            f.CompletePrevFeedback();
        }
    }
}
