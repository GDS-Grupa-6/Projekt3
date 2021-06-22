using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave45Anim : StateMachineBehaviour
{
    private BossWaves _bossWaves;
    private BossCombatLogic _combatLogic;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bossWaves = animator.GetComponent<BossWaves>();
        _combatLogic = animator.GetComponent<BossCombatLogic>();
        _bossWaves.ActiveWave(false);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bossWaves.ScaleWave(false);
    }
}
