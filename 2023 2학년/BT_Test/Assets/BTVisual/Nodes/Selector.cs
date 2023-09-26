using UnityEngine;

namespace BTVisual
{
    public class Selector : CompositeNode
    {
        protected int _current;

        protected override void OnStart()
        {
            _current = 0;
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            for (int i = _current; i < children.Count; ++i)
            {
                _current = i;
                var child = children[_current];

                switch (child.Update())
                {
                    case State.RUNNING:
                        return State.RUNNING;
                    case State.SUCCESS:
                        return State.SUCCESS;
                    case State.FAILURE:
                        continue;
                }
            }

            return State.FAILURE;
        }
    }
}

