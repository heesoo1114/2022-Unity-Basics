using UnityEngine;

public class PluggableBehaviorInputManager : MonoBehaviour
{
    public CharacterPluggableBehavior characterPB;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            characterPB.ChangeBehavior(new MoveBehavior());
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            characterPB.ChangeBehavior(new JumpBehavior());
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            characterPB.ChangeBehavior(new AttackBehavior(), prioritize: false);
        }
    }
}
