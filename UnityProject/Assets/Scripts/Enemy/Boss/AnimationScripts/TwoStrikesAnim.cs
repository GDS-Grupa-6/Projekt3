using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoStrikesAnim : StateMachineBehaviour
{
    private BossCombatLogic _combatLogic;
    private BossMovement _bossMovement;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bossMovement = animator.GetComponent<BossMovement>();
        _combatLogic = animator.GetComponent<BossCombatLogic>();
        _combatLogic.animationRepeds = Random.Range(1, 4);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_combatLogic.distanceToPlayer > 3)
        {
            _bossMovement.Walk();
        }
    }
}
