using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System.Linq;
using UnityEditor;
using UnityEngine;
using System;

namespace BTVisual
{
    public class BehaviourTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviourTreeView, UxmlTraits>{ }
        public new class UxmlTraits : GraphView.UxmlTraits { }

        private BehaviourTree _tree;
        public Action<NodeView> OnNodeSelected;

        public BehaviourTreeView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            Undo.undoRedoPerformed += OnUndoRedoHandle;
        }

        private void OnUndoRedoHandle()
        {
            PopulateView(_tree);
            AssetDatabase.SaveAssets();
        }

        // 그려주는 부분
        public void PopulateView(BehaviourTree tree)
        {
            _tree = tree;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements); // 그래프에서 그려진 모든 UI툴킷 요소들 삭제
            graphViewChanged += OnGraphViewChanged;

            if (_tree.rootNode == null)
            {
                _tree.rootNode = _tree.CreateNode(typeof(RootNode)) as RootNode;
                EditorUtility.SetDirty(_tree);
                AssetDatabase.SaveAssets();
            }

            tree.nodes.ForEach(n => CreateNodeView(n));

            tree.nodes.ForEach(n =>
            {
                var children = tree.GetChildren(n); // 이 노드에 대한 자식 전부 가져오기
                NodeView parent = FindNodeView(n);

                children.ForEach(c =>
                {
                    NodeView child = FindNodeView(c);

                    Edge edge = parent.output.ConnectTo(child.input);
                    AddElement(edge);
                });
                parent.SortChildren();
            });
        }

        private NodeView FindNodeView(Node n)
        {
            // 그래프에서 guid에 해당하는 것을 찾아서 가져옴
            return GetNodeByGuid(n.guid) as NodeView;
        }

        // 변경하는 부분
        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                graphViewChange.elementsToRemove.ForEach(elem =>
                {
                    var nv = elem as NodeView;
                    if (nv != null)
                    {
                        _tree.DeleteNode(nv.node);
                    }

                    var edge = elem as Edge;
                    if (edge != null)
                    {
                        NodeView parent = edge.output.node as NodeView;
                        NodeView child = edge.input.node as NodeView;

                        _tree.RemoveChild(parent.node, child.node);
                    }
                });
            }

            if (graphViewChange.edgesToCreate != null)
            {
                graphViewChange.edgesToCreate.ForEach(edge =>
                {
                    NodeView parent = edge.output.node as NodeView;
                    NodeView child = edge.input.node as NodeView;

                    _tree.AddChild(parent.node, child.node);
                    parent.SortChildren();
                });
            }

            if (graphViewChange.movedElements != null)
            {
                // 내가 이동했으면 부모만 정렬하면 돼
                nodes.ForEach(n =>
                {
                    var view = n as NodeView;
                    view?.SortChildren();
                });
            }

            return graphViewChange;
        }

        private void CreateNodeView(Node n)
        {
            NodeView nodeview = new NodeView(n);
            nodeview.OnNodeSelected = OnNodeSelected;
            AddElement(nodeview);
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            if (_tree == null)
            {
                evt.StopPropagation();
                return;
            }

            Vector2 mousePos = this.ChangeCoordinatesTo(contentViewContainer, evt.localMousePosition);

            {
                var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
                foreach (var t in types)
                {
                    evt.menu.AppendAction($"[ {t.BaseType.Name} ]/{t.Name}", (a) => CreateNode(t, mousePos));
                }
            }
            {
                var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
                foreach (var t in types)
                {
                    evt.menu.AppendAction($"[ {t.BaseType.Name} ]/{t.Name}", (a) => CreateNode(t, mousePos));
                }
            }
            {
                var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
                foreach (var t in types)
                {
                    evt.menu.AppendAction($"[ {t.BaseType.Name} ]/{t.Name}", (a) => CreateNode(t, mousePos));
                }
            }
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            // 인풋 아웃풋
            // 땡기고 있는 인풋 땡기고 아웃풋

            return ports.ToList().Where(p => p.direction != startPort.direction && p.node != startPort.node).ToList();
        }

        private void CreateNode(Type t, Vector2 createPos)
        {
            Node node = _tree.CreateNode(t);
            node.position = createPos;  
            CreateNodeView(node);
        }

        public void UpdateNodeStates()
        {
            nodes.ForEach(n =>
            {
                var nv = n as NodeView;
                nv?.UpdateState();
            });
        }
    }
}
