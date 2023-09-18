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
            nodes.Add(node);

            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(Node node)
        {
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            var decorator = parent as DecoratorNode;
            if (decorator != null)
            {
                decorator.child = child;
                return;
            }

            var rootNode = parent as RootNode;
            if (rootNode != null)
            {
                rootNode.child = child;
            }

            var composite = parent as CompositeNode;
            if (composite != null)
            {
                composite.children.Add(child);
                return;
            }
        }

        public void RemoveChild(Node parent, Node child)
        {
            var decorator = parent as DecoratorNode;
            if (decorator != null)
            {
                decorator.child = null;
                return;
            }

            var rootNode = parent as RootNode;
            if (rootNode != null)
            {
                rootNode.child = null;
                return;
            }

            var composite = parent as CompositeNode;
            if (composite != null)
            {
                composite.children.Remove(child);
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
    }
}
