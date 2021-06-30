using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave360Anim : StateMachineBehaviour
{
    private BossWaves _bossWaves;
    private BossCombatLogic _combatLogic;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bossWaves = animator.GetComponent<BossWaves>();
        _combatLogic = animator.GetComponent<BossCombatLogic>();
        _bossWaves.ActiveWave(true);

        if (_combatLogic.startPhase)
        {
            _combatLogic.startPhase = false;
            animator.SetBool("ToPlayer", false);
        }

        _bossWaves.StartWave(true);
    }
}
