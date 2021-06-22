using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMoveAnim : StateMachineBehaviour
{
    private BossMovement _bossMovement;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bossMovement = animator.GetComponent<BossMovement>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bossMovement.Walk();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
