namespace BTVisual.BasicNode
{
    public class RepeatNode : DecoratorNode
    {
        protected override void OnStart()
        {
            // do nothing
        }

        protected override void OnStop()
        {
            // do nothing
        }

        protected override State OnUpdate()
        {
            child.Update();
            return State.RUNNING;
        }
    }
}

