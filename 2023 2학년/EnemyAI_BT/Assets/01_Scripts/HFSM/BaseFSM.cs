using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFSM<T> : MonoBehaviour
{
    public class StateTransitionInfo
    {
        public T nowState { get; set; }
        public T nextState { get; set; }

        public StateTransitionInfo(T nowState, T nextState)
        {
            this.nowState = nowState;
            this.nextState = nextState;
        }

        // MonoBehaviour 에서 상속받아옴
        public override int GetHashCode()
        {
            return 16 + 30 * this.nowState.GetHashCode() + 30 * this.nextState.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            StateTransitionInfo other = obj as StateTransitionInfo;
            return other != null
                && this.nowState.Equals(other.nowState)
                && this.nextState.Equals(other.nextState);
        }
    }

    protected Dictionary<StateTransitionInfo, T> dictionaryTransition;

    public T nowState;
    public T previousState;

    // T 자료형 확인
    protected BaseFSM()
    {
        // T의 타입
        if (!typeof(T).IsEnum)
        {
            Debug.Log($"{typeof(T).FullName} is not state");
        }
    }

    // 다음 상태 정보를 가져옴
    private T GetNextState(T nextState)
    {
        // 전이 만들기
        StateTransitionInfo transitionInfo = new StateTransitionInfo(nowState, nextState);

        T refNextState;

        // TryGetValue : Dictionary에 key가 존재하면 true와 value를 반환, 존재하지 않으면 false와 0 반환

        // 전이가 그릇에 있는지 확인
        if (!dictionaryTransition.TryGetValue(transitionInfo, out refNextState))
        {
            Debug.Log($"Invaild State transition from {nowState} to {nextState}");
        }
        else
        {
            Debug.Log($"Next state {refNextState}");
        }

        return refNextState;
    }

    public bool SearchNextState(T nextState)
    {
        StateTransitionInfo stateTransitionInfo = new StateTransitionInfo(nowState, nextState);
        T refNExtState;
        
        if (!dictionaryTransition.TryGetValue(stateTransitionInfo, out refNExtState))
        {
            Debug.Log($"Invalid State transition from {nowState} to {refNExtState}");
            return false;
        }
        else
        {
            return true;
        }
    }

    // 다음 상태로 이동
    public T NextStateMove(T nextState)
    {
        previousState = nowState;
        nowState = GetNextState(nextState);
        Debug.Log($"Change State from {previousState} to {nowState}");
        return nowState;
    }
}
