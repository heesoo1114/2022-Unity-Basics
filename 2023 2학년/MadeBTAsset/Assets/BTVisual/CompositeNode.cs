using System.Collections.Generic;

namespace BTVisual
{
    public abstract class CompositeNode : Node
    {
        public List<Node> children = new List<Node>();
    }
}
