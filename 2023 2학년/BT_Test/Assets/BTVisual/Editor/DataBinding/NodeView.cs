using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace BTVisual
{
    public class NodeView : UnityEditor.Experimental.GraphView.Node
    {
        public Node node;
        public Port input;
        public Port output;
        public Action<NodeView> OnNodeSelected;
        public NodeView(Node node) : base("Assets/BTVisual/Editor/NodeView/NodeView.uxml")
        {
            this.node = node;
            this.title = node.name;
            this.viewDataKey = node.guid; //고유 아이디 통일
            style.left = node.position.x;
            style.top = node.position.y;

            CreateInputPorts();
            CreateOutputPorts();
            SetupClasses();

            Label descLabel = this.Q<Label>("description");
            descLabel.bindingPath = "description";
            descLabel.Bind(new SerializedObject(node)); //이렇게 하면 노드 오브젝트와 바인딩되서 값이 갱신돼
        }

        private void SetupClasses()
        {
            if (node is ActionNode)
            {
                AddToClassList("action");
            }else if (node is CompositeNode)
            {
                AddToClassList("composite");
            }else if (node is DecoratorNode)
            {
                AddToClassList("decorator");
            }else if (node is RootNode)
            {
                AddToClassList("root");
            }
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);
            Undo.RecordObject(node, "BT(SetPosition)");
            node.position.x = newPos.xMin;
            node.position.y = newPos.yMin; //좌측 상단을 저장함. 좌표는 좌상부터 0, 0임
            
            EditorUtility.SetDirty(node);
        }

        public override void OnSelected()
        {
            base.OnSelected();
            OnNodeSelected?.Invoke(this);
        }

        private void CreateInputPorts()
        {
            //각 노드별로 만들어지는 포트가 달라야한다. 
            if (node is ActionNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }else if (node is CompositeNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }else if (node is DecoratorNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }else if (node is RootNode)
            {
                //No input
            }

            if (input != null)
            {
                input.portName = "";
                //input.style.flexDirection = FlexDirection.Column;
                inputContainer.Add(input);
            }
        }
        private void CreateOutputPorts()
        {
            //각 노드별로 만들어지는 포트가 달라야한다. 
            if (node is ActionNode)
            {
                //액션노드는 아웃풋이 없다.
            }else if (node is CompositeNode)
            {
                //컴포짓 노드는 여러개의 아웃풋을 가진다.
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
            }else if (node is DecoratorNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            }else if (node is RootNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            }
            
            if (output != null)
            {
                output.portName = "";
                //output.style.flexDirection = FlexDirection.ColumnReverse;
                output.style.marginLeft = new StyleLength(-15);
                outputContainer.Add(output);
            }
        }

        public void SortChildren()
        {
            var composite = node as CompositeNode;
            if (composite != null) //내가 만약 composite노드라면
            {
                composite.children.Sort(
                    (left, right) => left.position.x < right.position.x ? -1 : 1);
            }
        }

        public void UpdateState()
        {
            if (Application.isPlaying)
            {
                RemoveFromClassList("running");
                RemoveFromClassList("failure");
                RemoveFromClassList("success");
                
                switch (node.state)
                {
                    case Node.State.RUNNING:
                        if (node.started)
                        {
                            AddToClassList("running");    
                        }
                        break;
                    case Node.State.FAILURE:
                        AddToClassList("failure");
                        break;
                    case Node.State.SUCCESS:
                        AddToClassList("success");
                        break;
                }
            }
        }
    }
}
