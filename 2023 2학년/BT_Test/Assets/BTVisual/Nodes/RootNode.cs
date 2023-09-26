namespace BTVisual
{
    public class RootNode : Node
    {
        // 하나의 차일드를 가지고 계속 실행해주는 노드 
        public Node child;
        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            return child.Update();
        }

        public override Node Clone()
        {
            RootNode node = Instantiate(this);
            node.child = child.Clone(); //차일드도 클로닝
            return node;
        }
    }
}
