using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    private List<Feedback> _feedbackList;

    private void Awake()
    {
        _feedbackList = new List<Feedback>();
        GetComponents<Feedback>(_feedbackList); // 나한테 붙어있는 모든 피드백 가져오기
    }

    public void PlayFeedback()
    {
        FinishFeedback();
        foreach (Feedback f in _feedbackList)
        {
            f.CreateFeedback();
        }
    }

    public void FinishFeedback()
    {
        foreach (Feedback f in _feedbackList)
        {
            f.FinishFeedback();
        }
    }
}
