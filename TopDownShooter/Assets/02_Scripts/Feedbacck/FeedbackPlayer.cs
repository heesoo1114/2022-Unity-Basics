using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPlayer : MonoBehaviour
{
    [SerializeField]
    private List<Feedback> _feedbackToPlay = null;
    
    public void PlayFeedback()
    {
        FinishFeedback(); // 들어가기전에 이전 피드백을 종료해주고 시작한다.
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
