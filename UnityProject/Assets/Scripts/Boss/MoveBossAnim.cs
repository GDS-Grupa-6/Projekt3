using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveBossAnim : StateMachineBehaviour
{
    private BossCombatLogic _combatLogic;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _combatLogic = animator.GetComponent<BossCombatLogic>();
        _combatLogic.StartMoveTimer();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _combatLogic.StopMoveTimer();
    }
}
