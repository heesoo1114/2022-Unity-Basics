using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    // 반대로 만들어줌
    public class Invertor : Node
    {
        protected Node _node;

        public Invertor(Node node)
        {
            _node = node;
        }

        public override NodeState Evaluate()
        {
            switch (_node.Evaluate())
            {
                case NodeState.RUNNING:
                    _nodeState = NodeState.RUNNING;
                    break;
                case NodeState.SUCCESS:
                    _nodeState = NodeState.FAILURE;
                    break;
                case NodeState.FAILURE:
                    _nodeState = NodeState.SUCCESS;
                    break;
            }

            return _nodeState;
        }
    }
}