using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public abstract class CompositeNode : Node
    {
        [HideInInspector] public List<Node> children = new List<Node>();
    }
}
