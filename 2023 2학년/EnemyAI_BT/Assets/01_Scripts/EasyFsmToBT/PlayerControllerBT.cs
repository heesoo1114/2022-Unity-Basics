using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBT : MonoBehaviour
{
    private BehaviorTree behaviorTree;

    private void Start()
    {
        behaviorTree = new BehaviorTree();

        BT_Node idleNode = new IdleAction(behaviorTree);
        BT_Node runNode = null;
        BT_Node jumpNode = new JumpAction(behaviorTree);

        BT_Node stateConditionNode = new StateCondition(behaviorTree);

        Selector stateSelectorNode = new Selector(behaviorTree, new BT_Node[] { idleNode, runNode, jumpNode });

        Sequncer sequenceNode = new Sequncer(behaviorTree, new BT_Node[] {stateConditionNode, stateSelectorNode});

        behaviorTree.Root = sequenceNode;

        StartCoroutine(RunBehaviorTree());
    }

    private IEnumerator RunBehaviorTree()
    {
        while (true)
        {
            BT_Node.Result result = behaviorTree.Root.Execute();
            Debug.Log($"Root Result : {result}");
            yield return null;

            if (result != BT_Node.Result.Running)
            {
                Debug.Log($"Behavior has finished with : {result}");
                break;
            }
        }
    }
}
