using UnityEngine;

namespace BTVisual
{
    public abstract class Node : ScriptableObject
    {
        public enum State
        {
            RUNNING,
            FAILURE,
            SUCCESS
        }

        public State state = State.RUNNING;
        public bool started = false;

        public State Update()
        {
            if (!started)
            {
                OnStart();
                started = true;

            }
            state = OnUpdate();

            if (state == State.FAILURE || state == State.SUCCESS)
            {
                OnStop();
                started = false;
            }

            return state;
        }

        public void Breaking()
        {
            OnStop();
            started = false;
        }

        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();
    }
}