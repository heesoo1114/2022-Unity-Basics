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

        // MonoBehaviour ���� ��ӹ޾ƿ�
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

    // T �ڷ��� Ȯ��
    protected BaseFSM()
    {
        // T�� Ÿ��
        if (!typeof(T).IsEnum)
        {
            Debug.Log($"{typeof(T).FullName} is not state");
        }
    }

    // ���� ���� ������ ������
    private T GetNextState(T nextState)
    {
        // ���� �����
        StateTransitionInfo transitionInfo = new StateTransitionInfo(nowState, nextState);

        T refNextState;

        // TryGetValue : Dictionary�� key�� �����ϸ� true�� value�� ��ȯ, �������� ������ false�� 0 ��ȯ

        // ���̰� �׸��� �ִ��� Ȯ��
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

    // ���� ���·� �̵�
    public T NextStateMove(T nextState)
    {
        previousState = nowState;
        nowState = GetNextState(nextState);
        Debug.Log($"Change State from {previousState} to {nowState}");
        return nowState;
    }
}
