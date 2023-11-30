using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    private List<Feedback> _feedbackToPlay = new List<Feedback>();

    private void Awake()
    {
        GetComponents<Feedback>(_feedbackToPlay);
    }

    public void PlayFeedback()
    {
        FinishFeedback();
        foreach (Feedback f in _feedbackToPlay)
        {
            f.CreateFeedback();
        }
    }

    public void FinishFeedback()
    {
        foreach (Feedback f in _feedbackToPlay)
        {
            f.CompletePrevFeedback();
        }
    }
}