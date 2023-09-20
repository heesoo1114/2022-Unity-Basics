using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BTVisual
{
    [CreateAssetMenu(menuName ="BehaviourTree/Tree")]
    public class BehaviourTree : ScriptableObject
    {
        public Node rootNode;
        public Node.State treeState = Node.State.RUNNING;

        public List<Node> nodes = new List<Node>();

        public BlackBoard blackboard = new BlackBoard();

        public Node.State Updaate()
        {
            if (rootNode.state == Node.State.RUNNING)
            {
                treeState = rootNode.Update();
            }
            // 루트가 러닝이 끝나면 트리의 수행이 끝남
            return treeState;
        }

        public Node CreateNode(System.Type type)
        {
            var node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            Undo.RecordObject(this, "BT(CreateNode)");
            nodes.Add(node);

            if (!Application.isPlaying)
            {
                AssetDatabase.AddObjectToAsset(node, this);
            }

            AssetDatabase.AddObjectToAsset(node, this);
            Undo.RegisterCreatedObjectUndo(node, "BT(CreateNode)");
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(Node node)
        {
            Undo.RecordObject(this, "BT(DeleteNode)");
            nodes.Remove(node);
            // AssetDatabase.RemoveObjectFromAsset(node);
            Undo.DestroyObjectImmediate(node); // Undo를 통해 삭제해야 되돌리기가 가능해짐
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            var decorator = parent as DecoratorNode;
            if (decorator != null)
            {
                Undo.RecordObject(decorator, "BT(AddChild)");
                decorator.child = child;
                EditorUtility.SetDirty(decorator);
                return;
            }

            var rootNode = parent as RootNode;
            if (rootNode != null)
            {
                Undo.RecordObject(rootNode, "BT(AddChild)");
                rootNode.child = child;
                EditorUtility.SetDirty(rootNode);
                return;
            }

            var composite = parent as CompositeNode;
            if (composite != null)
            {
                Undo.RecordObject(composite, "BT(AddChild)");
                composite.children.Add(child);
                EditorUtility.SetDirty(composite);
                return;
            }
        }

        public void RemoveChild(Node parent, Node child)
        {
            var decorator = parent as DecoratorNode;
            if (decorator != null)
            {
                Undo.RecordObject(decorator, "BT(RemoveChild)");
                decorator.child = null;
                EditorUtility.SetDirty(decorator);
                return;
            }

            var rootNode = parent as RootNode;
            if (rootNode != null)
            {
                Undo.RecordObject(rootNode, "BT(RemoveChild)");
                rootNode.child = null;
                EditorUtility.SetDirty(rootNode);
                return;
            }

            var composite = parent as CompositeNode;
            if (composite != null)
            {
                Undo.RecordObject(composite, "BT(RemoveChild)");
                composite.children.Remove(child);
                EditorUtility.SetDirty(composite);
                return;
            }
        }

        public List<Node> GetChildren(Node parent)
        {
            List<Node> children = new List<Node>();

            var composite = parent as CompositeNode;
            if (composite != null)
            {
                return composite.children;
            }

            var rootNode = parent as RootNode;
            if (rootNode != null && rootNode.child != null)
            {
                children.Add(rootNode.child);
            }

            var decorator = parent as DecoratorNode;
            if (decorator != null && decorator.child != null)
            {
                children.Add(decorator.child);
            }
            
            return children;
        }

        public void Traverse(Node node, System.Action<Node> visitor)
        {
            if (node)
            {
                visitor.Invoke(node);
                var children = GetChildren(node);
                children.ForEach(n => Traverse(n, visitor));
            }
        }

        public BehaviourTree Clone()
        {
            var tree = Instantiate(this);
            tree.rootNode = tree.rootNode.Clone();
            
            tree.nodes = new List<Node>();
            Traverse(tree.rootNode, n => tree.nodes.Add(n));
            
            return tree;
        }

        public void Bind(EnemyBrain brain)
        {
            Traverse(rootNode, n =>
            {
                n.blackBoard = blackboard;
                n.brain = brain;
            });
        }
    }
}
