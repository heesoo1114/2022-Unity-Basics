using Codice.CM.Common.Tree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class RootNode : Node
    {
        public Node child;

        protected override void OnStart()
        {

        }

        protected override void OnStop()
        {

        }

        public override Node Clone()
        {
            RootNode node = Instantiate(this);
            node.child  = child.Clone();
            return node;
        }

        protected override State OnUpdate()
        {
            return child.Update();
        }
    }
}
