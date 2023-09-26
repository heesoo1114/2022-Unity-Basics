using UnityEngine;

namespace BTVisual
{
    public abstract class DecoratorNode : Node
    {
        [HideInInspector] public Node child;
        
        public override Node Clone()
        {
            DecoratorNode node = Instantiate(this);
            node.child = child.Clone(); //차일드도 클로닝
            return node;
        }
    }
    
}