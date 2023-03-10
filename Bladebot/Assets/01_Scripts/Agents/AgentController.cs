using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    private Dictionary<StateType, IState> _stateDictionary = null;
    private IState _currentState;

    private void Awake()
    {
        _stateDictionary = new Dictionary<StateType, IState>();

        Transform stateTrm = transform.Find("States");

        foreach (StateType state in Enum.GetValues(typeof(StateType)))
        {
            IState stateScript = stateTrm.GetComponent($"{state}State") as IState;

            if (stateScript == null)
            {
                Debug.LogError($"There is no script : {state}");
                return;
            }
            stateScript.SetUp(transform);
            _stateDictionary.Add(state, stateScript);
        }
    }

    private void Start()
    {
        ChangeState(StateType.Normal); 
    }

    private void ChangeState(StateType state)
    {
        _currentState?.OnExitState();
        _currentState = _stateDictionary[state];
        _currentState.OnEnterState();
    }

    private void Update()
    {
        _currentState.UpdateState();
    }
}