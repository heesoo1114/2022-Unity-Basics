using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class PrioritySelectorNode : CompositeNode
    {
        
        protected override void OnStart()
        {
            //_current = 0;
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            for (int i = 0; i < children.Count; ++i)
            {
                var child = children[i];
                
                switch (child.Update())
                {
                    case State.RUNNING:
                        return State.RUNNING;
                    case State.FAILURE:
                        //실패하면 아무것도 안함. 다음노드로 바로 넘어간다.
                        break;
                    case State.SUCCESS:
                        return State.SUCCESS;
                }
            }
            
            return State.FAILURE; //다 돌아도 실패면 실패
        }
    }
}