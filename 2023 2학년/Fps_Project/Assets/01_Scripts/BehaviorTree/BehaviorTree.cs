using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BehaviorTree : MonoBehaviour
{
    private BT_Node mRoot;
    public BT_Node Root => mRoot;

    private bool startBehavior;
    private Coroutine behavior;

    public Dictionary<string, object> blackBoard { get; set; }

    private void Start()
    {
        blackBoard = new Dictionary<string, object>();
        blackBoard.Add("WorldBounds", new Rect(0, 0, 5, 5));
        startBehavior = false;
        // mRoot = new BT_Node(this);
        mRoot = new Repeater(this, new Sequncer(this, new BT_Node[] {new RandomWalk(this)}));
    }

    private IEnumerator RunBehavior()
    {
        BT_Node.Result result = Root.Execute();
        while (result == BT_Node.Result.Running)
        {
            Debug.Log($"Root Result : {result}");
            yield return null; // 한 프레임
            result = Root.Execute();
        }

        Debug.Log($"Behavior has finished with : {result}");
    }

    private void Update()
    {
        if (!startBehavior)
        {
            behavior = StartCoroutine(RunBehavior());
            startBehavior = true;
        }
    }
}
