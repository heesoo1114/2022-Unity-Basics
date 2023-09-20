using UnityEngine;

namespace BTVisual
{
    public class BehaviourTreeRunner : MonoBehaviour
    {
        public BehaviourTree tree;
        public EnemyBrain _brain;

        private void Start()
        {
            tree = tree.Clone();
            tree.Bind(_brai
        }

        private void Update()
        {
            tree.Updaate();
        }
    }
}
