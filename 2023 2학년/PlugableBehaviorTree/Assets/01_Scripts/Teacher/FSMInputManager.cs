using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMInputManager : MonoBehaviour
{
    public CharacterFSM characterFSM;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            characterFSM.ChangeState(CharacterState.Move);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            characterFSM.ChangeState(CharacterState.Jump);
        }
        else
        {
            characterFSM.ChangeState(CharacterState.Idle);
        }
    }
}
