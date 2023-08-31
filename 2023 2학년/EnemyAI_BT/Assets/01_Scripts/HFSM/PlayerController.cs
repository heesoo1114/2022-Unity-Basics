using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void Start()
    {
        BaseFsm_sub hfsm = new BaseFsm_sub();
        Debug.Log($"Now State : {hfsm.nowState}");
        Debug.Log($"Command.begin : now state : {hfsm.NextStateMove(PlayerState.Run)}");
        Debug.Log($"Invalid transition : {hfsm.SearchNextState(PlayerState.Idle)}");
        Debug.Log($"Previous state : {hfsm.previousState}");
    }
}
