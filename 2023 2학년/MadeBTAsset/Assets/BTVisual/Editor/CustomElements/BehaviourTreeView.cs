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

        // �׷��ִ� �κ�
        public void PopulateView(BehaviourTree tree)
        {
            _tree = tree;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements); // �׷������� �׷��� ��� UI��Ŷ ��ҵ� ����
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
                var children = tree.GetChildren(n); // �� ��忡 ���� �ڽ� ���� ��������
                NodeView parent = FindNodeView(n);

                children.ForEach(c =>
                {
                    NodeView child = FindNodeView(c);

                    Edge edge = parent.output.ConnectTo(child.input);
                    AddElement(edge);
                });
            });
        }

        private NodeView FindNodeView(Node n)
        {
            // �׷������� guid�� �ش��ϴ� ���� ã�Ƽ� ������
            return GetNodeByGuid(n.guid) as NodeView;
        }

        // �����ϴ� �κ�
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

            {
                var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
                foreach (var t in types)
                {
                    evt.menu.AppendAction($"[ {t.BaseType.Name} ]/{t.Name}", (a) => CreateNode(t));
                }
            }
            {
                var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
                foreach (var t in types)
                {
                    evt.menu.AppendAction($"[ {t.BaseType.Name} ]/{t.Name}", (a) => CreateNode(t));
                }
            }
            {
                var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
                foreach (var t in types)
                {
                    evt.menu.AppendAction($"[ {t.BaseType.Name} ]/{t.Name}", (a) => CreateNode(t));
                }
            }
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            // ��ǲ �ƿ�ǲ
            // ����� �ִ� ��ǲ ����� �ƿ�ǲ

            return ports.ToList().Where(p => p.direction != startPort.direction && p.node != startPort.node).ToList();
        }

        private void CreateNode(Type t)
        {
            Node node = _tree.CreateNode(t);
            CreateNodeView(node);
        }
    }
}
