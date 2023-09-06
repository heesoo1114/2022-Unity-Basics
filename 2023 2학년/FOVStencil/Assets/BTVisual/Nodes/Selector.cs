namespace BTVisual
{
    public class Selector : CompositeNode
    {
        protected int current;

        protected override void OnStart()
        {
            current = 0;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            for (int i = current; i < children.Count; ++i)
            {
                current = i;
                var child = children[current];

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

