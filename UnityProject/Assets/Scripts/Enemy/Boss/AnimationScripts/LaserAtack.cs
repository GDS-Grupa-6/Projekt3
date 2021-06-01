using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAtack : StateMachineBehaviour
{
    BossCombatLaser combatLaser;
    BossCombatLogic combatLogic;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        combatLaser = animator.GetComponent<BossCombatLaser>();
        combatLogic = animator.GetComponent<BossCombatLogic>();
        combatLaser.spinNumber = 0;
        combatLaser.StartCoroutine(combatLaser.ChangeLaserModeCourutine());
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (combatLaser.spinNumber < combatLaser.maxNumberOfSpin)
        {
            combatLaser.CreateLaser();
            combatLaser.SpinBoss();
        }
        else
        {
            combatLaser.DestroyLaser();
            combatLogic.ChangeBossState(BossState.Normal);
        }
    }
}
