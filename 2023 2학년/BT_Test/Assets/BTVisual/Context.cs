using UnityEngine;
using UnityEngine.AI;


namespace BTVisual
{
    public class Context 
    {
        public GameObject gameObject;
        public Transform transform;
        public Animator animator;
        public Rigidbody physics;
        public NavMeshAgent agent;
        public Collider collider;
        public CharacterController characterController;
        
        
        public static Context CreateFromGameObject(GameObject gameObject) {
            Context context = new Context();
            context.gameObject = gameObject;
            context.transform = gameObject.transform;
            context.animator = gameObject.GetComponent<Animator>();
            context.physics = gameObject.GetComponent<Rigidbody>();
            context.agent = gameObject.GetComponent<NavMeshAgent>();
            context.collider = gameObject.GetComponent<Collider>();
            context.characterController = gameObject.GetComponent<CharacterController>();
            
            //더 필요한게 있다면 여기다가 적어준다.

            return context;
        }
    }
}
