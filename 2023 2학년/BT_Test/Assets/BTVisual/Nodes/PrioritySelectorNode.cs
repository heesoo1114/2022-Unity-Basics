using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BTVisual
{
    public class PrioritySelectorNode : CompositeNode
    {
        private int _beforeIndex = 0;   
        protected override void OnStart()
        {
            //_current = 0;
            _beforeIndex = 0;
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
                        if (_beforeIndex != i)
                        {
                            children[_beforeIndex].Break();
                        }
                        Debug.Log($"{i}, {_beforeIndex}");
                        _beforeIndex = i;
                        return State.RUNNING;
                    case State.FAILURE:
                        //실패하면 아무것도 안함. 다음노드로 바로 넘어간다.
                        break;
                    case State.SUCCESS:
                        if (_beforeIndex != i)
                        {
                            children[_beforeIndex].Break();
                        }
                        _beforeIndex = i;
                        return State.SUCCESS;
                }
            }
            
            return State.FAILURE; //다 돌아도 실패면 실패
        }
    }
}