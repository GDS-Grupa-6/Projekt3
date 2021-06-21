using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaJumpAnim : StateMachineBehaviour
{
    private BossMovement _bossMovement;
    private BossCombatLogic _combatLogic;
    private Vector3 _startBossPos;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bossMovement = animator.GetComponent<BossMovement>();
        _combatLogic = animator.GetComponent<BossCombatLogic>();
        _startBossPos = animator.transform.position;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bossMovement.ParabolaJump(_combatLogic.targetTransform.position, _startBossPos);
        animator.SetBool("JumpEnd", _bossMovement.parabolaJumpEnded);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (animator.GetBool("ToPlayer"))
        {
            _combatLogic.ChangeFirstPhaseState(BossFirstPhaseStates.Wave360);
        }
        else
        {
            _combatLogic.ChangeFirstPhaseState(BossFirstPhaseStates.Puke);
        }
    }
}
