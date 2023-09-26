using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BTVisual
{
    [CreateAssetMenu(menuName = "BehviourTree/Tree")]
    public class BehaviourTree : ScriptableObject
    {
        public Node treeRoot;
        public Node.State treeState = Node.State.RUNNING;
        public List<Node> nodes = new List<Node>();
        public BlackBoard blackboard = new BlackBoard();

        public Node.State Update()
        {
            if (treeRoot.state == Node.State.RUNNING)
            {
                treeState = treeRoot.Update();
            }
            return treeState;
        }

#if UNITY_EDITOR
        /// <summary>
        /// 해당 타입의 노드를 생성함. 생성된 노드는 타입을 이름으로 받고, GUID를 생성하여 받음.
        /// </summary>
        /// <param name="type">노드 타입을 받도록 되어있음.</param>
        /// <returns>생성된 노드객체를 반환</returns>
        public Node CreateNode(System.Type type)
        {
            var node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            
            Undo.RecordObject(this, "BT (CreateNode)");
            nodes.Add(node);

            if (!Application.isPlaying)
            {
                AssetDatabase.AddObjectToAsset(node, this);    
            }

            Undo.RegisterCreatedObjectUndo(node, "BT (CreateNode)");
            
            AssetDatabase.SaveAssets();
            return node;
        }
        
        /// <summary>
        /// 지정된 노드를 삭제함
        /// </summary>
        /// <param name="node">삭제하고자 하는 노드</param>
        public void DeleteNode(Node node)
        {
            Undo.RecordObject(this, "BT (DeleteNode)");
            nodes.Remove(node);
            //AssetDatabase.RemoveObjectFromAsset(node);
            Undo.DestroyObjectImmediate(node);
            AssetDatabase.SaveAssets();
        }

        public void AddChild(Node parent, Node child)
        {
            //패런트의 타입에 따라 다르게 넣어줘야 한다.
            var decorator = parent as DecoratorNode;
            if (decorator != null) //데코레이터 노드라면
            {
                Undo.RecordObject(decorator, "BT (AddChild)");
                decorator.child = child;
                EditorUtility.SetDirty(decorator);
                return;
            }

            var rootNode = parent as RootNode;
            if (rootNode != null)
            {
                Undo.RecordObject(rootNode, "BT (AddChild)");
                rootNode.child = child;
                EditorUtility.SetDirty(rootNode);
            }
            
            var composite = parent as CompositeNode;
            if (composite != null) //콤포짓 노드라면
            {
                Undo.RecordObject(composite, "BT (AddChild)");
                composite.children.Add(child);
                EditorUtility.SetDirty(composite);
                return;
            }
        }

        public void RemoveChild(Node parent, Node child)
        {
            //패런트의 타입에 따라 다르게 삭제
            var decorator = parent as DecoratorNode;
            if (decorator != null) //데코레이터 노드라면
            {
                Undo.RecordObject(decorator, "BT (RemoveChild)");
                decorator.child = null;
                EditorUtility.SetDirty(decorator);
                return;
            }
            
            var rootNode = parent as RootNode;
            if (rootNode != null)
            {
                Undo.RecordObject(rootNode, "BT (RemoveChild)");
                rootNode.child = null;
                EditorUtility.SetDirty(rootNode);
                return;
            }
            
            var composite = parent as CompositeNode;
            if (composite != null) //콤포짓 노드라면
            {
                Undo.RecordObject(composite, "BT (RemoveChild)");
                composite.children.Remove(child);
                EditorUtility.SetDirty(composite);
                return;
            }
        }

        public List<Node> GetChildren(Node parent)
        {
            List<Node> children = new List<Node>();
            
            var composite = parent as CompositeNode;
            if (composite != null) //콤포짓 노드라면
            {
                return composite.children;
            }
            
            var rootNode = parent as RootNode;
            if (rootNode != null && rootNode.child != null)
            {
                children.Add(rootNode.child);
            }
            
            var decorator = parent as DecoratorNode;
            if (decorator != null && decorator.child != null) //데코레이터 노드라면
            {
                children.Add(decorator.child);
            }
            return children;
        }
#endif
        public void Traverse(Node node, System.Action<Node> visitor)
        {
            //노드를 순회하면서 각 노드들을 tree.nodes 리스트에 넣어주는 함수
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
            tree.treeRoot = tree.treeRoot.Clone();
            //트리 리스트에 있던 노드들도 새롭게 클로닝 된 노드들로 변경되어야 한다.
            tree.nodes = new List<Node>();
            Traverse(tree.treeRoot, n =>
            {
                tree.nodes.Add(n);
            });
            return tree;
        }

        public void Bind(Context context, EnemyBrain brain)
        {
            Traverse(treeRoot, n =>
            {
                n.blackboard = blackboard;
                n.context = context;
                n.brain = brain;
            });
        }
    }
    
}