using UnityEngine;

public class PlayerTransition : MonoBehaviour
{
    public void ChangeState(ref PlayerState currentState, PlayerState targetState, PlayerState contidionState = PlayerState.None)
    {
        if (contidionState == PlayerState.None)
        {
            currentState = targetState;
            Debug.Log(currentState);
        }
        else
        {
            if (currentState == contidionState)
            {
                currentState = targetState;
                Debug.Log(currentState);
            }
        }
    }
}
