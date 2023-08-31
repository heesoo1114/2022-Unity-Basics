using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public enum NodeState
    {
        SUCCESS = 1,
        FAILURE = 2,
        RUNNING = 3
    }

    // for debugging
    public enum NodeActionCode
    {
        None = 0,
        Chasing = 1,
        Shoot = 2,
    }

    public abstract class Node 
    {
        protected NodeState _nodeState;
        public NodeState NodeState => _nodeState;

        // for debugging
        protected NodeActionCode _code = NodeActionCode.None;

        public abstract NodeState Evaluate();
    }
}

