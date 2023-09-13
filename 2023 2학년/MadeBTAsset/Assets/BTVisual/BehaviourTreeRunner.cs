using BTVisual.BasicNode;
using UnityEngine;

namespace BTVisual
{
    public class BehaviourTreeRunner : MonoBehaviour
    {
        private BehaviourTree _tree;

        private void Start()
        {
            _tree = ScriptableObject.CreateInstance<BehaviourTree>();

            var wNode1 = ScriptableObject.CreateInstance<WaitNode>();
            wNode1.duration = 1;
            var node1 = ScriptableObject.CreateInstance<DebugNode>();
            node1.message = "Hello GGM BT 1!";

            var wNode2 = ScriptableObject.CreateInstance<WaitNode>();
            wNode2.duration = 2;
            var node2 = ScriptableObject.CreateInstance<DebugNode>();
            node2.message = "Hello GGM BT 2!";

            var wNode3 = ScriptableObject.CreateInstance<WaitNode>();
            wNode3.duration = 3;
            var node3 = ScriptableObject.CreateInstance<DebugNode>();
            node3.message = "Hello GGM BT 3!";

            var seq = ScriptableObject.CreateInstance<SequenceNode>();
            seq.children.Add(wNode1);
            seq.children.Add(node1);
            seq.children.Add(wNode2);
            seq.children.Add(node2);
            seq.children.Add(wNode3);
            seq.children.Add(node3);








            var rNode = ScriptableObject.CreateInstance<RepeatNode>();
            rNode.child = seq;

            _tree.rootNode = rNode;
        }

        private void Update()
        {
            _tree.Updaate();
        }
    }
}
