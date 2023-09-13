using UnityEngine;

namespace BTVisual
{
    public abstract class DecoratorNode : Node
    {
        [HideInInspector] public Node child;
    }
}
