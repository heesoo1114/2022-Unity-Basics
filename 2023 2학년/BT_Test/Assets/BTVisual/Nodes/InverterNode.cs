namespace BTVisual  
{
    public class Inverter : DecoratorNode {
        protected override void OnStart() {
        }

        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            switch (child.Update()) {
                case State.RUNNING:
                    return State.RUNNING;
                case State.FAILURE:
                    return State.SUCCESS;
                case State.SUCCESS:
                    return State.FAILURE;
            }
            return State.FAILURE;
        }
    }
    
}


