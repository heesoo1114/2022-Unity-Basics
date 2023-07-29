using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.RestService;
using UnityEngine;

public class RunStateBehaviour : StateMachineBehaviour
{
    private PlayerVFXManager _vfxManager;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_vfxManager == null)
        {
            _vfxManager = animator.transform.parent.GetComponent<PlayerVFXManager>();
        }

        _vfxManager.UpdateFootSetp(true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _vfxManager?.UpdateFootSetp(false);
    }
}
