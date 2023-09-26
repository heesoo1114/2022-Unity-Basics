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

        [HideInInspector] public State state = State.RUNNING;
        [HideInInspector] public bool started = false;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
        [HideInInspector] public BlackBoard blackboard;
        [HideInInspector] public EnemyBrain brain;
        [HideInInspector] public Context context;
        [TextArea] public string description;

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

        public virtual Node Clone()
        {
            return Instantiate(this);
        }

        public void Break()
        {
            OnStop();
            state = State.SUCCESS;
            started = false;
        }
        
        protected abstract void OnStart();
        protected abstract void OnStop();
        protected abstract State OnUpdate();
    }
    
}

